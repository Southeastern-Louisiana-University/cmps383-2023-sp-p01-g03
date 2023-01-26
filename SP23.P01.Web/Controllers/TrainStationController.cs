using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Models.DTOs;

namespace SP23.P01.Web.Controllers;

[ApiController]
[Route("api/stations")]
public class TrainStationController : ControllerBase
{
    // Make property to hold DataContext
    private readonly DataContext _dataContext;

    // Put DataContext into property
    public TrainStationController(DataContext dataContext)
    {
        this._dataContext = dataContext;
    }


    /// <returns>All entries in the TrainStations table</returns>
    [HttpGet]
    public async Task<ActionResult<TrainStationDto[]>> GetAllTrainStations()
    {
        var trainStationEntities = await _dataContext.TrainStations.ToListAsync();

        var trainStationDtos = new List<TrainStationDto>();
        foreach (var trainStationEntity in trainStationEntities)
        {
            trainStationDtos.Add(new TrainStationDto()
            {
                Id = trainStationEntity.Id,
                Name = trainStationEntity.Name,
                Address = trainStationEntity.Address,
            });
        }

        return Ok(trainStationDtos);
    }

    /// <returns>The TrainStation or 404</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TrainStationDto>> GetTrainStationById(int id)
    {
        var trainStationEntity = await _dataContext.TrainStations.FindAsync(id);

        if (trainStationEntity == null)
        {
            return NotFound();
        }

        var trainStationDto = new TrainStationDto()
        {
            Id = trainStationEntity.Id,
            Name = trainStationEntity.Name,
            Address = trainStationEntity.Address
        };

        return Ok(trainStationDto);
    }
}