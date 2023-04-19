using CarFleet.Data.Models;
using CarFleet.Data.Repository;
using CarFleet.Data.ViewModels;

namespace CarFleet.Service.Services;

public interface ICarService
{
    Task<List<CarBrandModel>> GetCars(string? brandName);

    Task<bool> Create(NewCarModel car);

    Task<bool> Update(CarViewModel car);

    Task<bool> Delete(string carId);
}

public class CarService : ICarService
{
    private readonly ICarRepository carRepo;

    public CarService(ICarRepository _carRepo)
    {
        carRepo = _carRepo;
    }

    public async Task<List<CarBrandModel>> GetCars(string? brandName)
    {
        if (brandName == null || brandName == string.Empty)
            return await carRepo.GetCar();
        return await carRepo.GetCarByBrandName(brandName!);
    }

    public async Task<bool> Create(NewCarModel car)
    {
        return await carRepo.Create(car.toCar(car));
    }

    public async Task<bool> Update(CarViewModel carModel)
    {
        return await carRepo.Update(carModel);
    }

    public async Task<bool> Delete(string carId)
    {
        var car = await GetCarById(new Guid(carId));
        if (car == null)
            throw new NullReferenceException();
        return await carRepo.Delete(car);
    }

    private async Task<Car?> GetCarById(Guid carId)
    {
        return await carRepo.GetCarById(carId);
    }
}