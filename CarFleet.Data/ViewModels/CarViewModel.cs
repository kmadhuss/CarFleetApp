using CarFleet.Data.Models;
using static CarFleet.Data.Declaration.Enums;

namespace CarFleet.Data.ViewModels;

public class CarViewModel : NewCarModel
{
    public Guid id { get; set; }
}

public class NewCarModel
{
    public Guid brandId { get; set; }

    public string modelName { get; set; } = string.Empty;
    public string colorName { get; set; } = string.Empty;
    public string colorCode { get; set; } = string.Empty;
    public DateTime launchDate { get; set; }
    public DateTime releaseDate { get; set; }
    public string fuelType { get; set; } = string.Empty;
    public string transmissionType { get; set; } = string.Empty;

    public Car toCar(NewCarModel carModel)
    {
        Car car = new Car
        {
            brandId = carModel.brandId,
            modelName = carModel.modelName,
            colorName = carModel.colorName,
            colorCode = carModel.colorCode,
            launchDate = carModel.launchDate,
            releaseDate = carModel.releaseDate,
        };
        car.fuelType = (FuelType)Enum.Parse(typeof(FuelType), carModel.fuelType, true);
        car.transmissionType = (TransmissionType)Enum.Parse(typeof(TransmissionType), carModel.transmissionType, true);
        return car;
    }
}

public class CarBrandModel : CarViewModel
{
    public string brandName { get; set; } = string.Empty;
}