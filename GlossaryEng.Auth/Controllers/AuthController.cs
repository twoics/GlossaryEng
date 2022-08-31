using AutoMapper;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Filters;
using GlossaryEng.Auth.Models.Requests;
using GlossaryEng.Auth.Services.Authenticator;
using GlossaryEng.Auth.Services.TokenValidator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlossaryEng.Auth.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<UserDb> _userManager;
    private readonly IMapper _mapper;
    private readonly IAuthenticator _authenticator;
    private readonly ITokenValidator _tokenValidator;
    
    public AuthController(UserManager<UserDb> userManager, IMapper mapper, IAuthenticator authenticator,
        ITokenValidator tokenValidator)
    {
        _userManager = userManager;
        _mapper = mapper;
        _authenticator = authenticator;
        _tokenValidator = tokenValidator;
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

        UserDb? user = await _userManager.Users.FirstOrDefaultAsync(
            u => u.RefreshTokenDb.Token == requestToken);
        
        if (user is null)
        {
            return NotFound("Token is nonexistent");
        }
        
        return Ok(await _authenticator.AuthenticateUserAsync(user));
    }
}