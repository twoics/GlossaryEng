using GlossaryEng.Auth.Data;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.CustomResult;
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

    public async Task<CustomResult> DeleteByTokenIdAsync(Guid id)
    {
        RefreshTokenDb? refreshTokenDb = await _usersDbContext.RefreshTokens.FindAsync(id);

        if (refreshTokenDb is null)
        {
            return new CustomResult(false, "Token isn't found");
        }

        _usersDbContext.RefreshTokens.Remove(refreshTokenDb);
        await _usersDbContext.SaveChangesAsync();
        
        return new CustomResult(true);
    }

    public async Task<RefreshTokenDb?> GetByTokenAsync(string refreshToken)
    {
        return await _usersDbContext.RefreshTokens.FirstOrDefaultAsync(
            t => t.Token == refreshToken);
    }

    public async Task DeleteAllUserTokensAsync(UserDb user)
    {
        IEnumerable<RefreshTokenDb> refreshTokensDb = await _usersDbContext.RefreshTokens
            .Where(t => t.UserDbId == user.Id)
            .ToListAsync();

        _usersDbContext.RefreshTokens.RemoveRange(refreshTokensDb);
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