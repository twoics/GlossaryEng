namespace GlossaryEng.Auth.Data.Entities;

public class RefreshToken
{
    public RefreshToken(string token, string userDbId)
    {
        Token = token;
        UserDbId = userDbId;
    }
    
    public int Id { get; set; }
    public string Token { get; set; }
    public string UserDbId { get; set; }
}