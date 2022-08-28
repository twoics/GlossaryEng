using AutoMapper;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Requests;
using GlossaryEng.Auth.Models.TokenGenerator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GlossaryEng.Auth.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<UserDb> _userManager;
    private readonly IMapper _mapper;
    private readonly ITokenGenerator _tokenGenerator;
    
    public AuthController(UserManager<UserDb> userManager, IMapper mapper, ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        UserDb user = _mapper.Map<UserDb>(registerRequest);

        IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User is successfully created");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

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
        
        return Ok("Welcome");
    }
}