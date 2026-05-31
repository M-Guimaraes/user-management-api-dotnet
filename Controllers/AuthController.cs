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
        bool result = await authService.Register(dto);

        if (!result) {
            return BadRequest();
        }
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        AuthResponseDto? result = await authService.Login(dto);
        
        if (result == null) {
            return Unauthorized();
        }
        
        return Ok(result);        
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenDto dto)
    {
        AuthResponseDto? result =  await authService.RefreshToken(dto);
        
        if (result == null) return Unauthorized();

        return Ok(result);       
    }
    
    [HttpPost("logout")]
    public async Task<ActionResult<bool>> Logout(RefreshTokenDto dto)
    {
        bool result = await authService.Logout(dto);
        
        if (!result) return NotFound();
        
        return NoContent();
    }
}
