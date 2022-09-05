using AutoMapper;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Filters;
using GlossaryEng.Auth.Models.CustomResult;
using GlossaryEng.Auth.Models.Requests;
using GlossaryEng.Auth.Services.Authenticator;
using GlossaryEng.Auth.Services.EmailSender;
using GlossaryEng.Auth.Services.TokenValidator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GlossaryEng.Auth.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<UserDb> _userManager;
    private readonly IMapper _mapper;
    private readonly IAuthenticator _authenticator;
    private readonly ITokenValidator _tokenValidator;
    private readonly IEmailSender _emailSender;

    public AuthController(UserManager<UserDb> userManager, IMapper mapper, IAuthenticator authenticator,
        ITokenValidator tokenValidator, IEmailSender emailSender)
    {
        _userManager = userManager;
        _mapper = mapper;
        _authenticator = authenticator;
        _tokenValidator = tokenValidator;
        _emailSender = emailSender;
    }

    [HttpPost]
    [ValidateModel]
    [Route("register")]
    public async Task<ObjectResult> Register([FromBody] RegisterRequest registerRequest)
    {
        UserDb user = _mapper.Map<UserDb>(registerRequest);

        IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmUrl = Url.Action(
            "ConfirmEmail",
            "Account",
            new EmailConfirmRequest
            {
                Id = user.Id,
                Code = code
            },
            protocol: HttpContext.Request.Scheme);

        var customResult = await _emailSender.SendEmailAsync("glossaryeng@gmail.com", "Test",
            $"Test message <a href={confirmUrl}>Click</a>");

        if (!customResult.IsSuccess)
        {
            return BadRequest(customResult.Error);
        }

        return Ok("User is successfully created");
    }

    [HttpPost]
    [ValidateModel]
    [Route("login")]
    public async Task<ObjectResult> Login([FromBody] LoginRequest loginRequest)
    {
        UserDb user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user is null)
        {
            return NotFound("User doesn't found");
        }

        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed)
        {
            return Unauthorized("Email doesn't confirmed");
        }

        bool isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!isValidPassword)
        {
            return Unauthorized("Wrong password");
        }

        return Ok(await _authenticator.AuthenticateUserAsync(user));
    }

    [HttpPost]
    [ValidateModel]
    [Route("refresh")]
    public async Task<ObjectResult> Refresh([FromBody] RefreshRequest refreshRequest)
    {
        string requestToken = refreshRequest.Token;
        if (!_tokenValidator.ValidateRefreshToken(requestToken))
        {
            return BadRequest("Token is invalid");
        }

        UserDb? user = await _authenticator.GetUserFromRefreshToken(refreshRequest);
        if (user is null)
        {
            return NotFound("User doesn't found");
        }

        CustomResult result = await _authenticator.DeleteTokenAsync(refreshRequest);
        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(await _authenticator.AuthenticateUserAsync(user));
    }

    [HttpPost]
    [ValidateModel]
    [Route("logout")]
    public async Task<ObjectResult> Logout([FromBody] LogoutRequest logoutRequest)
    {
        CustomResult result = await _authenticator.LogoutAsync(logoutRequest);
        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok("Logout successfully completed");
    }
}