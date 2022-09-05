using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.CustomResult;
using GlossaryEng.Auth.Models.Requests;
using GlossaryEng.Auth.Models.Responses;
using GlossaryEng.Auth.Models.Token;
using GlossaryEng.Auth.Services.RefreshTokensRepository;
using GlossaryEng.Auth.Services.TokenGenerator;

namespace GlossaryEng.Auth.Services.Authenticator;

public class Authenticator : IAuthenticator
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public Authenticator(ITokenGenerator tokenGenerator, IRefreshTokenRepository refreshTokenRepository)
    {
        _tokenGenerator = tokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<AuthenticatedUserResponse> AuthenticateUserAsync(UserDb user)
    {
        TokenRefresh tokenRefresh = _tokenGenerator.GenerateRefreshToken(user);
        RefreshTokenDb refreshTokenDb = new RefreshTokenDb(tokenRefresh.TokenValue, user.Id);

        await _refreshTokenRepository.AddTokenAsync(refreshTokenDb);

        return new AuthenticatedUserResponse
        {
            TokenAccess = _tokenGenerator.GenerateAccessToken(user),
            TokenRefresh = tokenRefresh
        };
    }

    public async Task<CustomResult> DeleteTokenAsync(RefreshRequest tokenRefresh)
    {
        RefreshTokenDb? refreshTokenDb = await _refreshTokenRepository.GetByTokenAsync(tokenRefresh.Token);

        if (refreshTokenDb is null)
        {
            return new CustomResult(false, "Refresh token doesn't found");
        }

        await _refreshTokenRepository.DeleteByTokenIdAsync(refreshTokenDb.Id);

        return new CustomResult(true);
    }

    public async Task<CustomResult> LogoutAsync(LogoutRequest logoutRequest)
    {
        UserDb? user = await _refreshTokenRepository.GetUserByTokenAsync(logoutRequest.RefreshToken);

        if (user is null)
        {
            return new CustomResult(false, "User doesn't found");
        }

        await _refreshTokenRepository.DeleteAllUserTokensAsync(user);

        return new CustomResult(true);
    }

    public async Task<UserDb?> GetUserFromRefreshToken(RefreshRequest tokenRefresh)
    {
        return await _refreshTokenRepository.GetUserByTokenAsync(tokenRefresh.Token);
    }
}