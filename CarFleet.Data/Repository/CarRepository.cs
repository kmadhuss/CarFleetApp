using CarFleet.Data.Data;
using CarFleet.Data.Models;
using CarFleet.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CarFleet.Data.Repository;

public interface ICarRepository
{
    Task<List<CarBrandModel>> GetCar();

    Task<Car?> GetCarById(Guid carId);

    Task<List<CarBrandModel>> GetCarByBrandName(string brandName);

    Task<bool> Create(Car car);

    Task<bool> Update(CarViewModel car);

    Task<bool> Delete(Car car);
}

public class CarRepository : ICarRepository
{
    public readonly DatabaseContext dBContext;

    public CarRepository(DatabaseContext _dbContext)
    {
        dBContext = _dbContext;
    }

    public async Task<List<CarBrandModel>> GetCar()
    {
        return await dBContext.cars.Include(car => car.CarBrand).OrderByDescending(car => car.ModifiedDate).
            Select(car => new CarBrandModel
            {
                id = car.id,
                brandName = car.CarBrand.brandName,
                modelName = car.modelName,
                brandId = car.brandId,
                colorCode = car.colorCode,
                colorName = car.colorName,
                fuelType = car.fuelType.ToString(),
                transmissionType = car.transmissionType.ToString(),
                launchDate = car.launchDate,
                releaseDate = car.releaseDate
            }).ToListAsync();
    }

    public async Task<Car?> GetCarById(Guid carId)
    {
        return await dBContext.cars.FindAsync(carId);
    }

    public async Task<List<CarBrandModel>> GetCarByBrandName(string brandName)
    {
        return await dBContext.cars.Include(car => car.CarBrand).
            Where(car => car.CarBrand.brandName.Contains(brandName)).
            Select(car => new CarBrandModel
            {
                id = car.id,
                brandName = car.CarBrand.brandName,
                modelName = car.modelName,
                brandId = car.brandId,
                colorCode = car.colorCode,
                colorName = car.colorName,
                fuelType = car.fuelType.ToString(),
                transmissionType = car.transmissionType.ToString(),
                launchDate = car.launchDate,
                releaseDate = car.releaseDate
            }).ToListAsync();
    }

    public async Task<bool> Create(Car car)
    {
        car.createdDate = DateTime.Now;
        car.ModifiedDate = DateTime.Now;
        dBContext.cars.Add(car);
        await dBContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(CarViewModel carModel)
    {
        var oldCarModel = await GetCarById(carModel.id);
        if (oldCarModel == null)
            throw new NullReferenceException();
        dBContext.Entry(oldCarModel).State = EntityState.Detached;
        var car = carModel.toCar(carModel);
        car.id = oldCarModel.id;
        car.createdDate = oldCarModel.createdDate;
        car.ModifiedDate = DateTime.Now;
        dBContext.Entry(car).State = EntityState.Modified;
        await dBContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Car car)
    {
        dBContext.cars.Remove(car);
        await dBContext.SaveChangesAsync();
        return true;
    }
}