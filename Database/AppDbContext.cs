
using Flight_Alert_API.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flight_Alert_API.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
    public DbSet<Airport> Airports { get; set; }
    public DbSet<MonitoredRoute> MonitoredRoutes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MonitoredRoute>()
            .HasOne(m => m.OriginAirport)
            .WithMany()
            .HasForeignKey(m => m.OriginAirportId)
            .HasPrincipalKey(a => a.Id);
            
        modelBuilder.Entity<MonitoredRoute>()
            .HasOne(m => m.DestinationAirport)
            .WithMany()
            .HasForeignKey(m => m.DestinationAirportId)
            .HasPrincipalKey(a => a.Id);
    }
}