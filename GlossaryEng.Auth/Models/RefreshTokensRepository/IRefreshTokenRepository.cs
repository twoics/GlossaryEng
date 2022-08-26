using GlossaryEng.Auth.Data.Entities;

namespace GlossaryEng.Auth.Models.RefreshTokensRepository;

public interface IRefreshTokenRepository
{
    Task Create(RefreshToken refreshToken);

    Task Delete(int tokenId);
}