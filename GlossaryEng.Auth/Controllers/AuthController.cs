using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityUser = GlossaryEng.Auth.Data.Entities.IdentityUser;

namespace GlossaryEng.Auth.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
        => _userManager = userManager;

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        IdentityUser user = new IdentityUser
        {
            UserName = registerRequest.Name,
            Email = registerRequest.Email,
            RefreshToken = new RefreshToken
            {
                Token = "I'm a refresh token"
            }
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User is successfully created");
    }
}