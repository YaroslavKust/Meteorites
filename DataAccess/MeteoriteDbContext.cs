using Microsoft.EntityFrameworkCore;
using Meteorites.DataAccess.Models;

namespace Meteorites.DataAccess;

public class MeteoriteDbContext(DbContextOptions<MeteoriteDbContext> options) : DbContext(options)
{
    public DbSet<Meteorite> Meteorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meteorite>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.HasIndex(m => m.Name);
            entity.HasIndex(m => m.Year);
            
            entity
                .OwnsOne(m => m.Geolocation)
                .ToJson();

        });
    }
}
