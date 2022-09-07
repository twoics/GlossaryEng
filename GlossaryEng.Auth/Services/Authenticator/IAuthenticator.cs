using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.CustomResult;
using GlossaryEng.Auth.Models.Responses;

namespace GlossaryEng.Auth.Services.Authenticator;

public interface IAuthenticator
{
    /// <summary>
    /// Authenticates the user.
    /// Creates a pair of tokens, an access token and an refresh token.
    /// The refresh token is saved in the database.
    /// One user can contain several refresh tokens.
    /// </summary>
    /// <param name="user">User for authentication</param>
    /// <returns>Access and refresh tokens and the time when they expire</returns>
    Task<AuthenticatedUserResponse> AuthenticateUserAsync(UserDb user);

    /// <summary>
    /// Deletes a token from the database, finds a token by its value
    /// </summary>
    /// <param name="refreshToken">Refresh token</param>
    /// <returns>CustomResult object that contains the method execution status and error, if any</returns>
    Task<CustomResult> DeleteTokenAsync(string refreshToken);
    
    /// <summary>
    /// Logout user.
    /// Deletes ALL user refresh tokens.
    /// </summary>
    /// <param name="refreshToken">Refresh token</param>
    /// <returns>CustomResult object that contains the method execution status and error, if any</returns>
    Task<CustomResult> LogoutAsync(string refreshToken);
    
    /// <summary>
    /// Finds a user who has this refresh token
    /// </summary>
    /// <param name="refreshToken">Refresh token</param>
    /// <returns>User</returns>
    Task<UserDb?> GetUserFromRefreshTokenAsync(string refreshToken);
}