using GlossaryEng.Auth.Data;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace GlossaryEng.Auth.Models.RefreshTokensRepository;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private UsersDbContext _usersDbContext;

    public RefreshTokenRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task Create(RefreshToken refreshToken)
    {
        _usersDbContext.RefreshTokens.Add(refreshToken);
        await _usersDbContext.SaveChangesAsync();
    }

    public async Task Delete(RefreshRequest refreshRequest)
    {
        RefreshToken? refreshToken =
            await _usersDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshRequest.Token);

        if (refreshToken is not null)
        {
            _usersDbContext.RefreshTokens.Remove(refreshToken);
            await _usersDbContext.SaveChangesAsync();
        }
    }
}