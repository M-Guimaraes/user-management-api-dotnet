using System.ComponentModel.DataAnnotations;

namespace UserManagementApi;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    [DataType(DataType.Password)]
    public string PasswordHash { get; set; } = string.Empty;

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}
