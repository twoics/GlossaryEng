using System.ComponentModel.DataAnnotations;

namespace GlossaryEng.Auth.Models.Requests;

public class ChangeUsernameRequest
{
    [Required] 
    public string Email { get; set; }

    [Required] 
    public string RefreshToken { get; set; }

    [Required]
    public string NewUserName{ get; set; }

}