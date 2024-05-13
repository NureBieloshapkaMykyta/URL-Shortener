using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataAccess;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppUser>().
            HasMany(u=>u.Urls).
            WithOne(url=>url.Creator).
            HasForeignKey(u=>u.CreatorId);

        base.OnModelCreating(builder);
    }

    public DbSet<Url> Urls { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var updates = ChangeTracker.Entries<ChangesTrackingEntity>().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
        foreach (var item in updates)
        {
            item.Entity.ModifiedDate = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
