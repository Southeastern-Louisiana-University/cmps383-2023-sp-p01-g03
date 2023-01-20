using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Models.Entities;

namespace SP23.P01.Web.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        // Grab DbContext
        using var context = new DataContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<DataContext>>());

        // Look for any TrainStations.
        if (context.TrainStation.Any())
        {
            return;   // DB has been seeded
        }


        context.TrainStation.AddRange(
            new TrainStation
            {
                Id = 0,
                Name = "Station 0",
                Address = "123 Station Street"
            },
            new TrainStation
            {
                Id = 1,
                Name = "Station 1",
                Address = "123 Station Street"
            },
            new TrainStation
            {
                Id = 2,
                Name = "Station 2",
                Address = "123 Station Street"
            },
            new TrainStation
            {
                Id = 3,
                Name = "Station 3",
                Address = "123 Station Street"
            }
        );
        context.SaveChanges();
    }
}
