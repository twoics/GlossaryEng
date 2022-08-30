namespace GlossaryEng.Auth.Services.TokenValidator;

public interface ITokenValidator
{
    bool ValidateRefreshToken(string token);
}