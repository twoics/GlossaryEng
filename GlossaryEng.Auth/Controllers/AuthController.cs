using AutoMapper;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GlossaryEng.Auth.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private UserManager<UserDb> _userManager;
    private IMapper _mapper;
    
    public AuthController(UserManager<UserDb> userManager, IMapper mapper)
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

        UserDb user = _mapper.Map<UserDb>(registerRequest);

        IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User is successfully created");
    }
}