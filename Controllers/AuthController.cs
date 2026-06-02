using Microsoft.AspNetCore.Mvc;

using UserManagementApi.DTOs;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : Controller
{

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto, CancellationToken cancellationToken)
    {
        bool result = await authService.Register(dto, cancellationToken);

        if (!result) {
            return BadRequest();
        }
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        AuthResponseDto? result = await authService.Login(dto, cancellationToken);
        
        if (result == null) {
            return Unauthorized();
        }
        
        return Ok(result);        
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenDto dto, CancellationToken cancellationToken)
    {
        AuthResponseDto? result =  await authService.RefreshToken(dto, cancellationToken);

        if (result == null) {
            return Unauthorized();
        }

        return Ok(result);       
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RefreshTokenDto dto, CancellationToken cancellationToken)
    {
        bool result = await authService.Logout(dto, cancellationToken);

        if (!result) {
            return NotFound();
        };
        
        return NoContent();
    }
}
