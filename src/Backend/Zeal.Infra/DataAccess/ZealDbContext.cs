using Microsoft.EntityFrameworkCore;
using Zeal.Domain.Entities;

namespace Zeal.Infra.DataAccess;

public class ZealDbContext : DbContext
{
    public ZealDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Domain.Entities.User> Users { get; set; }
    public DbSet<Domain.Entities.Event> Events { get; set; }
    public DbSet<Domain.Entities.Address> Addresses{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZealDbContext).Assembly);
    }
}