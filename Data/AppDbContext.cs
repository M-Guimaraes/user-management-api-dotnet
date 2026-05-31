using Microsoft.EntityFrameworkCore;

namespace UserManagementApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

    public DbSet<User> Users => Set<User>();
    
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Users
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        
        // Refresh Tokens
        modelBuilder.Entity<RefreshToken>().ToTable("refresh_tokens");
        
    }
}
