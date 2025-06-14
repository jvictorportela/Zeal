using Microsoft.EntityFrameworkCore;

namespace Zeal.Infra.DataAccess;

public class ZealDbContext : DbContext
{
    public ZealDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Domain.Entities.User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZealDbContext).Assembly);
    }
}