namespace GlossaryEng.Auth.Models.Token;

public abstract class Token
{
    public string TokenValue { get; set; }
    public DateTime ExpireTime { get; set; }
}