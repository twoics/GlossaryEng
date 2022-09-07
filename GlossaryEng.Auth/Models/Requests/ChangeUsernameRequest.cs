using System.ComponentModel.DataAnnotations;

namespace GlossaryEng.Auth.Models.Requests;

public class ChangeUsernameRequest
{
    [Required]
    public string NewUserName{ get; set; }

    [Required] 
    public string RefreshToken { get; set; }

}