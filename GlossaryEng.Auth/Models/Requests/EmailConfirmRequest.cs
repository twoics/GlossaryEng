namespace GlossaryEng.Auth.Models.Requests;

public class EmailConfirmRequest
{
    public string Id { get; set; }
    public string Code { get; set; }
}