using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.DTOs;

public class CreateUserDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
