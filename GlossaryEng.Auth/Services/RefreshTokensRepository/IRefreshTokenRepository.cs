using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.CustomResult;

namespace GlossaryEng.Auth.Services.RefreshTokensRepository;

public interface IRefreshTokenRepository
{
    Task AddTokenAsync(RefreshTokenDb refreshTokenDb);
    
    Task<CustomResult> DeleteByTokenIdAsync(Guid id);

    Task<RefreshTokenDb?> GetByTokenAsync(string refreshToken);

    Task DeleteAllUserTokensAsync(UserDb user);

    Task<UserDb?> GetUserByTokenAsync(string refreshToken);
}