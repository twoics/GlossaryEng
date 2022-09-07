using System.ComponentModel.DataAnnotations;

namespace GlossaryEng.Auth.Models.Requests;

public class ChangePasswordRequest
{
    [Required] 
    public string Password { get; set; }
 
    [Required]
    public string NewPassword { get; set; }
    
    [Required]
    public string RefreshToken { get; set; }
}