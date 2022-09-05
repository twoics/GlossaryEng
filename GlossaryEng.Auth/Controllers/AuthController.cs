using AutoMapper;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Filters;
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
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        UserDb user = _mapper.Map<UserDb>(registerRequest);

        IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var customResult = await _emailSender.SendEmailAsync("glossaryeng@gmail.com", "Test",
            "Test message <a href=\"https://www.youtube.com/\">Тык</a>");

        if (!customResult.IsSuccess)
        {
            return BadRequest(customResult.Error);
        }
        
        return Ok("User is successfully created");
    }

    [HttpPost]
    [ValidateModel]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        UserDb user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user is null)
        {
            return Unauthorized("User doesn't found");
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
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
    {
        string requestToken = refreshRequest.Token;
        if (!_tokenValidator.ValidateRefreshToken(requestToken))
        {
            return BadRequest("Token is invalid");
        }

        RefreshTokenDb? refreshTokenDb = await _authenticator.DeleteTokenAsync(refreshRequest);

        if (refreshTokenDb is null)
        {
            return NotFound("Refresh token doesn't found");
        }

        UserDb? user = await _userManager.FindByIdAsync(refreshTokenDb.UserDbId);

        if (user is null)
        {
            return NotFound("User doesn't found");
        }

        return Ok(await _authenticator.AuthenticateUserAsync(user));
    }

    [HttpPost]
    [ValidateModel]
    [Route("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest logoutRequest)
    {
        UserDb? user = await _authenticator.LogoutAsync(logoutRequest);
        if (user is null)
        {
            return NotFound("Can't logout user. User doesn't found");
        }

        return Ok("Logout successfully completed");
    }
}