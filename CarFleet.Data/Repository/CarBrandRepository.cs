using CarFleet.Data.Data;
using CarFleet.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFleet.Data.Repository;

public interface ICarBrandRepository
{
    Task<List<CarBrand>> GetAll();

    Task<bool> Create(CarBrand carBrand);
    Task<bool> Update(CarBrand carBrand);
}

public class CarBrandRepository : ICarBrandRepository
{
    public readonly DatabaseContext dBContext;

    public CarBrandRepository(DatabaseContext _dbContext)
    {
        dBContext = _dbContext;
    }

    public async Task<List<CarBrand>> GetAll()
    {
        return await dBContext.carBrands.ToListAsync();
    }

    public async Task<bool> Create(CarBrand carBrand)
    {
        dBContext.carBrands.Add(carBrand);
        await dBContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(CarBrand carBrand)
    {
        dBContext.Entry(carBrand).State = EntityState.Modified;
        await dBContext.SaveChangesAsync();
        return true;
    }
}