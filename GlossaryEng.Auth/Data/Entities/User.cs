using Microsoft.AspNetCore.Identity;

namespace GlossaryEng.Auth.Data.Entities;

public class User : IdentityUser
{
    public RefreshToken RefreshToken { get; set; }
}