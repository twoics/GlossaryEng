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
    public async Task<ObjectResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        UserDb? user = await ValidateUser(changePasswordRequest.Email, changePasswordRequest.Password);
        if (user is null)
        {
            return NotFound("Invalid login or password");
        }
        
        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed)
        {
            return Unauthorized("Email doesn't confirmed");
        }
        
        IdentityResult result =
            await _userManager.ChangePasswordAsync(user, changePasswordRequest.Password,
                changePasswordRequest.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest("Can't change user password");
        }

        return Ok("Password has been changed");
    }
    
    
    [HttpPost]
    [ValidateModel]
    [Route("change-username")]
    public async Task<ObjectResult> ChangeUserName([FromBody] ChangeUsernameRequest changeUsernameRequest)
    {
        UserDb? user = await ValidateUser(changeUsernameRequest.Email, changeUsernameRequest.Password);
        if (user is null)
        {
            return NotFound("Invalid login or password");
        }

        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed)
        {
            return Unauthorized("Email doesn't confirmed");
        }
        
        IdentityResult result = await _userManager.SetUserNameAsync(user, changeUsernameRequest.NewUserName);
        if (!result.Succeeded)
        {
            return BadRequest("Can't change user name. Duplicate name");
        }

        return Ok("Username has been changed");

    }

    [HttpGet]
    [ValidateModel]
    public async Task<ObjectResult> ConfirmEmail(ConfirmEmail confirmEmail)
    { 
        UserDb? user = await _userManager.FindByIdAsync(confirmEmail.Id);
        if (user is null)
        {
            return NotFound("User doesn't found");
        }

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmail.Code);

        if (result.Succeeded)
        {
            return Ok("Done");
        }

        return BadRequest("Can't confirm email");
    }
    
    private async Task<UserDb?> ValidateUser(string email, string password)
    {
        UserDb? user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return null;
        }

        bool isValidPassword = await _userManager.CheckPasswordAsync(user, password);
        
        // if password invalid return null, else user
        return !isValidPassword ? null : user;
    }
}