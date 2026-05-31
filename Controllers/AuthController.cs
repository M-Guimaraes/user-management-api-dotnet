using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UserManagementApi.DTOs;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : Controller
{

    [HttpPost("register")]
    public async Task<ActionResult<bool>> Register(RegisterDto dto)
    {
        
        bool registered = await authService.Register(dto);

        if (!registered) {
            return BadRequest();
        }
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        AuthResponseDto? user = await authService.Login(dto);
        
        if (user == null) {
            return Unauthorized();
        }
        
        return Ok(user);        
    }
    
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(User.Claims.Select(c => new
        {
            c.Type,
            c.Value,
        }));
    }
}
