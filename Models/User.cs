using System.ComponentModel.DataAnnotations;

namespace UserManagementApi;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime? DeletedAt { get; set; } = null;
    
    public bool IsDeleted { get; set; } = false;
    
    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}
