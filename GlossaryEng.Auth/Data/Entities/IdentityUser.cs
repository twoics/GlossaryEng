using Microsoft.AspNetCore.Identity;

namespace GlossaryEng.Auth.Data.Entities;

public class IdentityUser : Microsoft.AspNetCore.Identity.IdentityUser
{
    public RefreshToken RefreshToken { get; set; }
}