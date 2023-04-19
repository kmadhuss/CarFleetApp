using CarFleet.Data.Models;
using CarFleet.Data.ViewModels;
using CarFleet.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarFleet.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[Produces("application/json")]
[Consumes("application/json")]
public class CarBrandController : ControllerBase
{
    private readonly ICarBrandService carBrandService;

    public CarBrandController(ICarBrandService _carBrandService)
    {
        carBrandService = _carBrandService;
    }

    [HttpGet("getCarBrand")]
    public async Task<IActionResult> GetAllCarBrand()
    {
        var result = await carBrandService.GetCarBrand();
        return Ok(result);
    }

    [HttpPost("insertCarBrand")]
    public async Task<IActionResult> CreateCarBrand(CarBrandViewModel carBrand)
    {
        var result = await carBrandService.CreateCarBrand(carBrand);
        if (result)
            return Ok("New car brand created successfully.");
        return BadRequest(result);
    }

    [HttpPut("updateCarBrand")]
    public async Task<IActionResult> Update(CarBrand carBrand)
    {
        var result = await carBrandService.Update(carBrand);
        if (result)
            return Ok("Car brand updated successfully.");
        return BadRequest(result);
    }
}