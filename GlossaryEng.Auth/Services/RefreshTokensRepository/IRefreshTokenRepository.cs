using GlossaryEng.Auth.Data.Entities;

namespace GlossaryEng.Auth.Services.RefreshTokensRepository;

public interface IRefreshTokenRepository
{
    Task AddTokenAsync(RefreshTokenDb refreshTokenDb);
    
    Task DeleteByTokenIdAsync(Guid id);

    Task<RefreshTokenDb?> GetByTokenAsync(string refreshToken);

    Task DeleteAllUserTokensAsync(UserDb user);

    Task<UserDb?> GetUserByTokenAsync(string refreshToken);
}