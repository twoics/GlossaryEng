using GlossaryEng.Auth.Models.Token;

namespace GlossaryEng.Auth.Models.Responses;

public class AuthenticatedUserResponse
{
    public TokenAccess TokenAccess { get; set; }
    public TokenRefresh TokenRefresh { get; set; }
}