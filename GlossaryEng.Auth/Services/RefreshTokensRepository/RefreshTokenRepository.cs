using GlossaryEng.Auth.Data;
using GlossaryEng.Auth.Data.Entities;
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

    public async Task DeleteByTokenIdAsync(Guid id)
    {
        RefreshTokenDb? refreshTokenDb = await _usersDbContext.RefreshTokens.FindAsync(id);

        if (refreshTokenDb is not null)
        {
            _usersDbContext.RefreshTokens.Remove(refreshTokenDb);
            await _usersDbContext.SaveChangesAsync();
        }
    }

    public async Task<RefreshTokenDb?> GetByTokenAsync(string refreshToken)
    {
        return await _usersDbContext.RefreshTokens.FirstOrDefaultAsync(
            t => t.Token == refreshToken);
    }

    public async Task DeleteAllUserTokensAsync(UserDb user)
    {
        IEnumerable<RefreshTokenDb> refreshTokensDbs = await _usersDbContext.RefreshTokens
            .Where(t => t.UserDbId == user.Id)
            .ToListAsync();

        _usersDbContext.RefreshTokens.RemoveRange(refreshTokensDbs);
        await _usersDbContext.SaveChangesAsync();
    }

    public async Task<UserDb?> GetUserByTokenAsync(string refreshToken)
    {
        RefreshTokenDb? refreshTokenDb = await GetByTokenAsync(refreshToken);

        if (refreshTokenDb is null)
        {
            return null;
        }

        return await _usersDbContext.Users.FirstOrDefaultAsync(
            u => u.Id == refreshTokenDb.UserDbId);
    }
}