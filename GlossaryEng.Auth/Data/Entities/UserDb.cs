using Microsoft.AspNetCore.Identity;

namespace GlossaryEng.Auth.Data.Entities;

public class UserDb : IdentityUser
{
    public RefreshToken RefreshToken { get; set; }
}