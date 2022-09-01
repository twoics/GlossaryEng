using GlossaryEng.Auth.Data;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace GlossaryEng.Auth.Services.RefreshTokensRepository;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private UsersDbContext _usersDbContext;

    public RefreshTokenRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task AddTokenAsync(RefreshTokenDb refreshTokenDb)
    {
        _usersDbContext.RefreshTokens.Add(refreshTokenDb);
        await _usersDbContext.SaveChangesAsync();
    }

    public async Task DeleteTokenIfExistAsync(UserDb user)
    {
        RefreshTokenDb? refreshToken =
            await _usersDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.UserDbId == user.Id);

        if (refreshToken is not null)
        {
            _usersDbContext.RefreshTokens.Remove(refreshToken);
            await _usersDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteByTokenIdAsync(Guid id)
    {
        RefreshTokenDb? refreshTokenDb = await _usersDbContext.RefreshTokens.FindAsync(id);

        if (refreshTokenDb is not null)
        {
            _usersDbContext.RefreshTokens.Remove(refreshTokenDb);
            await _usersDbContext.SaveChangesAsync();
        }
    }

    public async Task<RefreshTokenDb?> GetByTokenAsync(string token)
    {
        return await _usersDbContext.RefreshTokens.FirstOrDefaultAsync(
            t => t.Token == token); 
    }
}