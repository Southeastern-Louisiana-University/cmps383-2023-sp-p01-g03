using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Models.DTOs;

namespace SP23.P01.Web.Controllers;

[ApiController]
[Route("api/stations")]
public class TrainStationController : ControllerBase
{
    // Make property to hold DataContext
    private readonly DataContext dataContext;

    // Put DataContext into property
    public TrainStationController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }


    /// <returns>All entries in the TrainStations table</returns>
    [HttpGet]
    public async Task<List<TrainStationDto>> GetAllTrainStations()
    {
        var trainStationsToConvert = await dataContext.TrainStations.ToListAsync();

        var convertedTrainStations = new List<TrainStationDto>();
        foreach (var trainStation in trainStationsToConvert)
        {
            convertedTrainStations.Add(new TrainStationDto()
            {
                Id = trainStation.Id,
                Name = trainStation.Name,
                Address = trainStation.Address,
            });
        }

        return convertedTrainStations;
    }

    /// <returns>The TrainStation or 404</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TrainStationDto>> GetTrainStationById(int id)
    {
        var trainStationToConvert = await dataContext.TrainStations.FindAsync(id);

        if (trainStationToConvert == null)
        {
            return NotFound();
        }

        var convertedTrainStation = new TrainStationDto()
        {
            Id = trainStationToConvert.Id,
            Name = trainStationToConvert.Name,
            Address = trainStationToConvert.Address
        };

        return Ok(convertedTrainStation);
    }
}