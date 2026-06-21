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
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        
        // Refresh Tokens
        modelBuilder.Entity<RefreshToken>().ToTable("refresh_tokens");
        // Exclude revoked tokens and tokens belonging to deleted users
        modelBuilder.Entity<RefreshToken>().HasQueryFilter(rt => !rt.Revoked && !rt.User.IsDeleted);

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        
        foreach (var entry in ChangeTracker.Entries<BaseEntity>()) {
            switch (entry.State) {
                case EntityState.Modified:
                    entry.Property(e => e.CreatedAt).IsModified = false;
                    entry.Entity.UpdatedAt = now;
                    break;
                
                case EntityState.Added:
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.CreatedAt = now;
                    break;
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}
