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
        await authService.Register(dto, cancellationToken);

        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        AuthResponseDto? result = await authService.Login(dto, cancellationToken);
        
        return Ok(result);        
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenDto dto, CancellationToken cancellationToken)
    {
        AuthResponseDto? result =  await authService.RefreshToken(dto, cancellationToken);

        return Ok(result);       
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RefreshTokenDto dto, CancellationToken cancellationToken)
    {
        await authService.Logout(dto, cancellationToken);
        
        return NoContent();
    }
}
