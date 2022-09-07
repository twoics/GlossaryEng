using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.CustomResult;

namespace GlossaryEng.Auth.Services.RefreshTokensRepository;

public interface IRefreshTokenRepository
{
    /// <summary>
    /// Add refresh token to the refresh token database
    /// </summary>
    /// <param name="refreshTokenDb">Refresh Token</param>
    /// <returns></returns>
    Task AddTokenAsync(RefreshTokenDb refreshTokenDb);
    
    /// <summary>
    /// Deletes the refresh token from the database, by its GUID
    /// </summary>
    /// <param name="id">GUID of Refresh token</param>
    /// <returns>CustomResult object that contains the method execution status and error, if any</returns>
    Task<CustomResult> DeleteByTokenIdAsync(Guid id);

    /// <summary>
    /// Finds the refresh token object in the database by its value
    /// </summary>
    /// <param name="refreshToken">Refresh Token</param>
    /// <returns>Refresh token object</returns>
    Task<RefreshTokenDb?> GetByTokenAsync(string refreshToken);
    
    /// <summary>
    /// Deletes ALL refresh tokens that the user has
    /// </summary>
    /// <param name="user">User who has tokens to delete</param>
    /// <returns></returns>
    Task DeleteAllUserTokensAsync(UserDb user);
    
    /// <summary>
    /// Find the user who has the refresh token
    /// </summary>
    /// <param name="refreshToken">Refresh Token</param>
    /// <returns>User who has this token</returns>
    Task<UserDb?> GetUserByTokenAsync(string refreshToken);
}