using System.ComponentModel.DataAnnotations;

namespace SP23.P01.Web.Models.Entities;

public class TrainStation
{
    public int Id { get; set; }

    [StringLength(maximumLength: 120), Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }
}
