using CarFleet.Data.Models;
using CarFleet.Data.Repository;
using CarFleet.Data.ViewModels;

namespace CarFleet.Service.Services;

public interface ICarBrandService
{
    Task<bool> CreateCarBrand(CarBrandViewModel carBrand);

    Task<List<CarBrand>> GetCarBrand();
    Task<bool> Update(CarBrand carBrand);
}

public class CarBrandService : ICarBrandService
{
    private readonly ICarBrandRepository carBrandRepo;

    public CarBrandService(ICarBrandRepository _carBrandRepo)
    {
        carBrandRepo = _carBrandRepo;
    }

    public async Task<bool> CreateCarBrand(CarBrandViewModel carBrand)
    {
        return await carBrandRepo.Create(carBrand.ToCarBrand(carBrand));
    }

    public async Task<List<CarBrand>> GetCarBrand()
    {
        return await carBrandRepo.GetAll();
    }

    public async Task<bool> Update(CarBrand carBrand)
    {
        return await carBrandRepo.Update(carBrand);
    }

}