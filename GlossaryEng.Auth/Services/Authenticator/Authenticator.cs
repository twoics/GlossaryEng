using GlossaryEng.Auth.Data.Entities;
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
        await _refreshTokenRepository.DeleteTokenIfExistAsync(user);

        TokenRefresh tokenRefresh = _tokenGenerator.GenerateRefreshToken(user);
        RefreshToken refreshToken = new RefreshToken(tokenRefresh.TokenValue, user.Id);

        await _refreshTokenRepository.AddTokenAsync(refreshToken);

        return new AuthenticatedUserResponse
        {
            TokenAccess = _tokenGenerator.GenerateAccessToken(user),
            TokenRefresh = tokenRefresh
        };
    }
}