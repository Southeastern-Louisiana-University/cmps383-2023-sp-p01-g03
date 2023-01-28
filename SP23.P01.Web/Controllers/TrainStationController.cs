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
        var result = await _dataContext.TrainStations.Select(x => new TrainStationDto
        {
            Id = x.Id,
            Name = x.Name,
            Address = x.Address,
        }).ToArrayAsync();
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

        if (trainStationCreateDto.Name == null || trainStationCreateDto.Name == "")    // If the name for the new station is empty throw an error
        {
            return BadRequest();
        }

        if (trainStationCreateDto.Name.Length > 120)    // If the Name of the new station is longer than 120 characters then throw an error
        {
            return BadRequest();
        }

        if (trainStationCreateDto.Address == null || trainStationCreateDto.Address == "") // If the new station's address is empty then throw an error
        {
            return BadRequest();
        }

        var stationNameExists = _dataContext  // Checks for if the name actually exists and is in use
            .TrainStations
            .Any(x => x.Name == trainStationCreateDto.Name.Trim());

        var stationAddressExists = _dataContext     // Checks for if the address actually exists and is in use
            .TrainStations
            .Any(x => x.Address == trainStationCreateDto.Address.Trim());

        if (stationAddressExists)    // If address is in use then throw an error
        {
            return BadRequest();
        }

        if (stationNameExists)   // If in name is in use then throw an error
        {
            return BadRequest();
        }

        var trainStationToCreate = new TrainStation    // The data actually being added
        {
            Name = trainStationCreateDto.Name.Trim(),        // Trim removes dead space in the text
            Address = trainStationCreateDto.Address.Trim()
        };

        var trainStationReturn = new TrainStationDto     // The data to return to the URL
        {
            Id = trainStationToCreate.Id,
            Name = trainStationToCreate.Name.Trim(),
            Address = trainStationToCreate.Address.Trim()

        };

        var trainStationToReturn = _dataContext.TrainStations.Add(trainStationToCreate);
        _dataContext.SaveChanges();

        var location = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{this.Request.Path.Value}/{trainStationToReturn.Entity.Id}"; // Location to send information to

        return Created(location, new TrainStationDto() // Return the created Train Station
        {
            Id = trainStationToReturn.Entity.Id,
            Address = trainStationToReturn.Entity.Address.Trim(),
            Name = trainStationToReturn.Entity.Name.Trim()
        });
    }


}