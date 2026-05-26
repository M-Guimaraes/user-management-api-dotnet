
using Microsoft.AspNetCore.Mvc;

using UserManagementApi.DTOs;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public ActionResult<UserResponseDto> CreateUser([FromBody] CreateUserDto dto) 
    {
        var createdUser = _userService.Create(dto);
        var userResponse = new UserResponseDto {
            Id = createdUser.Id,
            Name = createdUser.Name,
            Email = createdUser.Email
        };
        
        return CreatedAtAction(
            nameof(GetUserById), 
            new {id = createdUser.Id},
            userResponse
            );
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<UserResponseDto> GetUserById(int id)
    {
        var user = _userService.GetById(id);
        if (user == null)
            return NotFound();

        var userRespose = new UserResponseDto {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
        
        return Ok(userRespose);
    }
        
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDto>> > GetUsers()
    {
        var users = await _userService.GetAllAsync();
        var usersResponse = users.Select(user => new UserResponseDto {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        });
        return Ok(usersResponse);
    }

    [HttpPut]
    public ActionResult<User> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        var updated = _userService.Update(id, dto);

        if (!updated)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete]
    public IActionResult DeleteUser(int id)
    {
        var deleted = _userService.Delete(id);
        
        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}
