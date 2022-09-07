namespace GlossaryEng.Auth.Services.TokenValidator;

public interface ITokenValidator
{
    /// <summary>
    /// Checks the token for validity. 
    /// </summary>
    /// <param name="token">Some token to check</param>
    /// <returns>Returns true - if the token is valid, else false</returns>
    bool ValidateRefreshToken(string token);
}