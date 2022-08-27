using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Requests;

namespace GlossaryEng.Auth.Models.RefreshTokensRepository;

public interface IRefreshTokenRepository
{
    Task Create(RefreshToken refreshToken);

    Task Delete(RefreshRequest refreshRequest);
}