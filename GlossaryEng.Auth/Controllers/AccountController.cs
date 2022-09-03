using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Filters;
using GlossaryEng.Auth.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GlossaryEng.Auth.Controllers;

[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<UserDb> _userManager;

    public AccountController(UserManager<UserDb> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    [ValidateModel]
    [Route("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        UserDb? user = await _userManager.FindByEmailAsync(changePasswordRequest.Email);
        if (user is null)
        {
            return NotFound("User doesn't found");
        }

        bool isValidPassword = await _userManager.CheckPasswordAsync(user, changePasswordRequest.Password);
        if (!isValidPassword)
        {
            return Unauthorized("Wrong password");
        }

        IdentityResult result =
            await _userManager.ChangePasswordAsync(user, changePasswordRequest.Password,
                changePasswordRequest.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest("Can't change user password");
        }

        return Ok("Password has changed");
    }
}