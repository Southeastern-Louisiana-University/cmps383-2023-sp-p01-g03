using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Models.Entities;

namespace SP23.P01.Web;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) { }

    // Add Entities here
    public DbSet<TrainStation> TrainStations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from Entity classes that extend IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
