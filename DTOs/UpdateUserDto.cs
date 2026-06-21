using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.DTOs;

public class UpdateUserDto
{
    public string? Name { get; set; } = null;
    
    public string? Email { get; set; } = null;
}
