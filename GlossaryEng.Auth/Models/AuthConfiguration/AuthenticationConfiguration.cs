namespace GlossaryEng.Auth.Models.AuthConfiguration;

public class AuthenticationConfiguration
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    
    public string AccessTokenSecret { get; set; }
    public double AccessTokenExpireMinutes { get; set; }

    public string RefreshTokenSecret { get; set; }
    public double RefreshTokenExpireMinutes { get; set; }

}