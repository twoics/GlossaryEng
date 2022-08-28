using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GlossaryEng.Auth.Models.AuthConfiguration;
using Microsoft.IdentityModel.Tokens;

namespace GlossaryEng.Auth.Services.TokenValidator;

public class TokenValidator : ITokenValidator
{
    private readonly AuthenticationConfiguration _authConfiguration;

    public TokenValidator(AuthenticationConfiguration authConfiguration) 
        => _authConfiguration = authConfiguration;


    public bool Validate(string token, string secretKey)
    {
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidIssuer = _authConfiguration.Issuer,
            ValidAudience = _authConfiguration.Audience,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}