using CarFleet.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarFleet.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService userService;

    public AuthController(IAuthService _userService)
    {
        userService = _userService;
    }

    [HttpGet("getToken")]
    public IActionResult Login()
    {
        return Ok(userService.GetToken());
    }
}