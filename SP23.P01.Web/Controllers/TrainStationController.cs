using Microsoft.AspNetCore.Mvc;
using SP23.P01.Web.Models.Entities;

namespace SP23.P01.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainStationController : ControllerBase
{

    private readonly ILogger<TrainStationController> _logger;

    public TrainStationController(ILogger<TrainStationController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<TrainStation> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new TrainStation
        {
            Id = Random.Shared.Next(-20, 55),
            Name = "Mark",
            Address = "Some Address"
        })
        .ToArray();
    }
}