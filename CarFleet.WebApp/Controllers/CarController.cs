using CarFleet.Data.ViewModels;
using CarFleet.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarFleet.Data.Declaration;
using Microsoft.OpenApi.Extensions;

namespace CarFleet.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[Produces("application/json")]
[Consumes("application/json")]
public class CarController : ControllerBase
{
    private readonly ICarService carService;

    public CarController(ICarService _carService)
    {
        carService = _carService;
    }

    [HttpGet("getCars")]
    public async Task<IActionResult> GetCars(string? brandName)
    {
        return Ok(await carService.GetCars(brandName));
    }

    [HttpPost("insertCar")]
    public async Task<IActionResult> CreateCar(NewCarModel car)
    {
        var result = await carService.Create(car);
        if (result)
            return Ok("New car model created successfully.");
        return BadRequest(result);
    }

    [HttpPut("updateCar")]
    public async Task<IActionResult> UpdateCar(CarViewModel car)
    {
        var result = await carService.Update(car);
        if (result)
            return Ok("Car model updated successfully.");
        return BadRequest(result);
    }

    [HttpDelete("deleteCar")]
    public async Task<IActionResult> DeleteCar(string carId)
    {
        var result = await carService.Delete(carId);
        if (result)
            return Ok("Car model deleted successfully.");
        return BadRequest(result);
    }

    [HttpGet("getFuelType")]
    public IActionResult GetAllFuelType()
    {

        return Ok(Enum.GetValues(typeof(Enums.FuelType))
            .Cast<Enums.FuelType>()
            .Select(e => e.GetDisplayName())
            .ToList());
    }
    
    [HttpGet("getTransmissionType")]
    public IActionResult GetAllTransmissionType()
    {
        return Ok(Enum.GetValues(typeof(Enums.TransmissionType))
            .Cast<Enums.TransmissionType>()
            .Select(e => e.GetDisplayName())
            .ToList());
    }
}