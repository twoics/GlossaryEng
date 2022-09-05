namespace GlossaryEng.Auth.Models.Requests;

public class ConfirmEmail
{
    public string Id { get; }
    public string Code { get; }

    public ConfirmEmail(string id, string code)
    {
        Id = id;
        Code = code;
    }
}