using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SP23.P01.Web.Models.Entities;

public class TrainStation
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
}

public class TrainStationConfiguration : IEntityTypeConfiguration<TrainStation>
{
    public void Configure(EntityTypeBuilder<TrainStation> builder)
    {
        // Name is required and has a max length of 120
        builder.Property(t => t.Name).IsRequired().HasMaxLength(120);

        // Address is required
        builder.Property(t => t.Address).IsRequired();
    }
}
