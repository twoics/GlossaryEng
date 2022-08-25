using System.ComponentModel.DataAnnotations;

namespace GlossaryEng.Auth.Models.Request;

public class RegisterRequest
{
    [Required(ErrorMessage = "Name field can't be null")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Password can't be null")]
    public string Password { get; set; }
    
    [Compare("Password")]
    [Required(ErrorMessage = "You must confirm the password")]
    public string ConfirmPassword { get; set; }
    
    [EmailAddress]
    [Required(ErrorMessage = "Email can't be null")]
    public string Email { get; set; }
}