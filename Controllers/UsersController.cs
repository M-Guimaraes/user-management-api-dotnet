
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UserManagementApi.DTOs;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("users")]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{

   
    [HttpGet("{id:int}")]
    public ActionResult<UserResponseDto> GetUserById(int id)
    {
        User? user = userService.GetById(id);
        if (user == null)
            return NotFound();

        var userResponse = new UserResponseDto {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
        };
        
        return Ok(userResponse);
    }
        
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDto>> > GetUsers()
    {
        var users = await userService.GetAllAsync();
        var usersResponse = users.Select(user => new UserResponseDto {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
        });
        return Ok(usersResponse);
    }

    [HttpPut]
    public ActionResult<User> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        bool updated = userService.Update(id, dto);

        if (!updated)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete]
    public IActionResult DeleteUser(int id)
    {
        bool deleted = userService.Delete(id);
        
        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}
