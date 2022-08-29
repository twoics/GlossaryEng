using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Requests;

namespace GlossaryEng.Auth.Services.RefreshTokensRepository;

public interface IRefreshTokenRepository
{
    Task AddTokenAsync(RefreshToken refreshToken);

    Task DeleteTokenIfExistAsync(UserDb user);
}