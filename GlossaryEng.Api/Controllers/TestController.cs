using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlossaryEngApi.Controllers;

[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [Route("hello")]
    public string Hello()
    {
        return "Hello world";
    }

    [Authorize]
    [Route("private")]
    public string Private()
    {
        return "Private method";
    }
}