namespace GlossaryEng.Auth.Data.Entities;

public class RefreshTokenDb
{
    public RefreshTokenDb(string token, string userDbId)
    {
        Token = token;
        UserDbId = userDbId;
    }
    
    public Guid Id { get; set; }
    public string Token { get; set; }
    public string UserDbId { get; set; }
}