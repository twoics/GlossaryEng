using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.AuthConfiguration;
using Microsoft.IdentityModel.Tokens;

namespace GlossaryEng.Auth.Models.TokenGenerator;

public class TokenGenerator : ITokenGenerator
{
    private AuthenticationConfiguration _configuration;

    public TokenGenerator(AuthenticationConfiguration configuration)
        => _configuration = configuration;

    public string GenerateAccessToken(UserDb user)
    {
        List<Claim> claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Name, user.UserName),
            // Todo add role claim
        };
        
        DateTime expireTime = DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpireMinutes);
        
        string token = GenerateToken(
            _configuration.AccessTokenSecret,
            _configuration.Issuer,
            _configuration.Audience,
            expireTime,
            claims);
        
        return token;
    }

    public string GenerateRefreshToken(UserDb user)
    {
        DateTime expireTime = DateTime.UtcNow.AddMinutes(_configuration.RefreshTokenExpireMinutes);
        
        return GenerateToken(
            _configuration.RefreshTokenSecret,
            _configuration.Issuer,
            _configuration.Audience,
            expireTime);
    }

    private string GenerateToken(
        string secretKey,
        string issuer,
        string audience,
        DateTime utcExpirationTime,
        IEnumerable<Claim>? claims = null)
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            DateTime.UtcNow,
            utcExpirationTime,
            signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}