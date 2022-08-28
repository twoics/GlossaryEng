namespace GlossaryEng.Auth.Services.TokenValidator;

public interface ITokenValidator
{
    bool Validate(string token, string secretKey);
}