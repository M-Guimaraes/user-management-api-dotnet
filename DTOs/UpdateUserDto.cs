using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.DTOs;

public class UpdateUserDto
{
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
