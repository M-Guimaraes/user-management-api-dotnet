namespace UserManagementApi;

public class User : BaseEntity
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    
    public DateTime? DeletedAt { get; set; } = null;
    
    public bool IsDeleted { get; set; } = false;
    
    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}
