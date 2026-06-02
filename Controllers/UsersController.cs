
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UserManagementApi.Common;
using UserManagementApi.DTOs;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("users")]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{

   
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponseDto>> GetUserById(int id, CancellationToken cancellationToken)
    {
        UserResponseDto? userResponse = await userService.GetByIdAsync(id, cancellationToken);
        
        if (userResponse == null)
            return NotFound();
        
        return Ok(userResponse);
    }
        
    [HttpGet]
    public async Task<ActionResult<PagedResult<UserResponseDto>> > GetUsers(
        [FromQuery] UserQueryDto query, 
        CancellationToken cancellationToken
        )
    {
        var usersResponse = await userService.GetAllAsync(query, cancellationToken);
        
        return Ok(usersResponse);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
    {
        bool updated = await userService.Update(id, dto, cancellationToken);

        if (!updated)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        bool deleted = await userService.Delete(id, cancellationToken);
        
        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}
