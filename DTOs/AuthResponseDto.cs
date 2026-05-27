using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.DTOs;

public class AuthResponseDto
{
    [Required]
    public string Token { get; set; } = string.Empty;
    
    [Required]
    public string Email { get; set; } = string.Empty;  
    
    [Required]
    public string Name { get; set; } = string.Empty; 
}
