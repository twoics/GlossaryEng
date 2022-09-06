using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Filters;
using GlossaryEng.Auth.Models.Requests;
using GlossaryEng.Auth.Services.Authenticator;
using GlossaryEng.Auth.Services.TokenValidator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GlossaryEng.Auth.Controllers;

[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<UserDb> _userManager;
    private readonly ITokenValidator _tokenValidator;
    private readonly IAuthenticator _authenticator;

    public AccountController(UserManager<UserDb> userManager, ITokenValidator tokenValidator,
        IAuthenticator authenticator)
    {
        _userManager = userManager;
        _tokenValidator = tokenValidator;
        _authenticator = authenticator;
    }

    [HttpPost]
    [ValidateModel]
    [Route("change-password")]
    public async Task<ObjectResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        UserDb? user = await _userManager.FindByEmailAsync(changePasswordRequest.Email);
        if (user is null)
        {
            return NotFound("Invalid token. No user has this token");
        }

        bool isValidate = _tokenValidator.ValidateRefreshToken(changePasswordRequest.RefreshToken);
        if (!isValidate)
        {
            return BadRequest("Token is invalid");
        }

        IdentityResult result =
            await _userManager.ChangePasswordAsync(user, changePasswordRequest.Password,
                changePasswordRequest.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest("Operation failed. Can't change user password");
        }

        await _authenticator.LogoutAsync(changePasswordRequest.RefreshToken);
        
        return Ok(await _authenticator.AuthenticateUserAsync(user));
    }

    [HttpPost]
    [ValidateModel]
    [Route("change-username")]
    public async Task<ObjectResult> ChangeUserName([FromBody] ChangeUsernameRequest changeUsernameRequest)
    {
        UserDb? user = await _userManager.FindByEmailAsync(changeUsernameRequest.Email);
        if (user is null)
        {
            return NotFound("User doesn't found");
        }

        bool isValid = _tokenValidator.ValidateRefreshToken(changeUsernameRequest.RefreshToken);
        if (!isValid)
        {
            return BadRequest("Token is invalid");
        }

        IdentityResult result = await _userManager.SetUserNameAsync(user, changeUsernameRequest.NewUserName);
        if (!result.Succeeded)
        {
            return BadRequest("Can't change user name. Duplicate name");
        }

        await _authenticator.LogoutAsync(changeUsernameRequest.RefreshToken);
        
        return Ok(await _authenticator.AuthenticateUserAsync(user));
    }

    [HttpGet]
    [ValidateModel]
    public async Task<ObjectResult> ConfirmEmail(EmailConfirmRequest emailConfirmRequest)
    { 
        UserDb? user = await _userManager.FindByIdAsync(emailConfirmRequest.Id);
        if (user is null)
        {
            return NotFound("User doesn't found");
        }

        var result = await _userManager.ConfirmEmailAsync(user, emailConfirmRequest.Code);
        if (result.Succeeded)
        {
            return Ok("Done");
        }

        return BadRequest("Can't confirm email");
    }
}