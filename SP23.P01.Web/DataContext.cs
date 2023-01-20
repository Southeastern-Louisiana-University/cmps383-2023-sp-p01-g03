using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Models.Entities;

namespace SP23.P01.Web;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) { }

    // Add Entities here
    public DbSet<TrainStation> TrainStations { get; set; }
}
