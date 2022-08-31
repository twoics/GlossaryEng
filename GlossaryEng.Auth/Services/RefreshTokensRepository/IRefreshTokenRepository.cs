using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Requests;

namespace GlossaryEng.Auth.Services.RefreshTokensRepository;

public interface IRefreshTokenRepository
{
    Task AddTokenAsync(RefreshTokenDb refreshTokenDb);

    Task DeleteTokenIfExistAsync(UserDb user);
}