using AutoMapper;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Request;
using GlossaryEng.Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityUser = GlossaryEng.Auth.Data.Entities.IdentityUser;

namespace GlossaryEng.Auth.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private UserManager<IdentityUser> _userManager;
    private IMapper _mapper;
    
    public AuthController(UserManager<IdentityUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        IdentityUser user = _mapper.Map<IdentityUser>(registerRequest);
        return Ok(user);
        //
        // IdentityUser user = new IdentityUser
        // {
        //     UserName = registerRequest.Name,
        //     Email = registerRequest.Email,
        //     RefreshToken = new RefreshToken
        //     {
        //         Token = "I'm a refresh token"
        //     }
        // };

        IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User is successfully created");
    }
}