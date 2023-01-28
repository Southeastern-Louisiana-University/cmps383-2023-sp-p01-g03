using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Models.DTOs;
using SP23.P01.Web.Models.Entities;
using System.Diagnostics;

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
        var result = _dataContext.TrainStations.Select(x => new TrainStationDto
        {
            Id = x.Id,
            Name = x.Name,
            Address = x.Address,
        }).ToArray();
        return Ok(result);
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

    [HttpPost]
    public IActionResult Create([FromBody] TrainStationDto trainStationCreateDto)
    {
        if (trainStationCreateDto == null)
        {
            return BadRequest();
        }

        if (trainStationCreateDto.Name == null || trainStationCreateDto.Name == "")
        {
            return BadRequest();
        }

        if (trainStationCreateDto.Name.Length > 120)
        {
            return BadRequest();
        }

        if (trainStationCreateDto.Address == null || trainStationCreateDto.Address == "")
        {
            return BadRequest();
        }

        var stationNameExists = _dataContext  // Checks for if the attribute actually exists
            .TrainStations
            .Any(x => x.Name == trainStationCreateDto.Name.Trim());

        var stationAddressExists = _dataContext
            .TrainStations
            .Any(x => x.Address == trainStationCreateDto.Address.Trim());

        if (stationAddressExists)
        {
            return BadRequest();
        }

        if (stationNameExists)
        {
            return BadRequest();
        }

        var trainStationToCreate = new TrainStation // The data actually being added
        {
            Name = trainStationCreateDto.Name.Trim(),
            Address = trainStationCreateDto.Address.Trim()
        };

        var trainStationReturn = new TrainStationDto    // The data to return to the URL
        {
            Id = trainStationToCreate.Id,
            Name = trainStationToCreate.Name.Trim(),
            Address = trainStationToCreate.Address.Trim()

        };

        var trainStationToReturn = _dataContext.TrainStations.Add(trainStationToCreate);
        _dataContext.SaveChanges();

        var location = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{this.Request.Path.Value}/{trainStationToReturn.Entity.Id}";

        return Created(location, new TrainStationDto()
        {
            Id = trainStationToReturn.Entity.Id,
            Address = trainStationToReturn.Entity.Address.Trim(),
            Name = trainStationToReturn.Entity.Name.Trim()
        });
    }

    
    [HttpPut("{id}")]
    public async Task<ActionResult<TrainStationDto>> UpdateTrainStationById(int id, TrainStationDto trainStationDto)
    {

        if (trainStationDto.Name.Length > 120)
        {
            return BadRequest();
        }

        var trainStation = await _dataContext.TrainStations.FindAsync(id);

        if (id != trainStation.Id)
        {
            return BadRequest();
        }
        _dataContext.Entry(trainStationDto).State = EntityState.Modified;

        try { await _dataContext.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException)
        {
            if (trainStation == null)
            {
                return NotFound();
            }
            else { throw; }
        }
        var TrainStationDto = new TrainStationDto()
        {
            Id = trainStation.Id,
            Name = trainStation.Name,
            Address = trainStation.Address,
        };

        return Ok(TrainStationDto);
    }

}