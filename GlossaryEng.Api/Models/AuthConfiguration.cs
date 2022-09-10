namespace GlossaryEngApi.Models;

public class AuthConfiguration
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string AccessTokenSecret { get; set; }
}