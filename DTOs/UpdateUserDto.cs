using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.DTOs;

public class UpdateUserDto
{
    [MinLength(3)]
    public string? Name { get; set; } = null;
    
    [EmailAddress]
    public string? Email { get; set; } = null;
}
