using CarFleet.Data.Models;

namespace CarFleet.Data.ViewModels;

public class CarBrandViewModel
{
    public string brandName { get; set; } = string.Empty;
    public string logoUrl { get; set; } = string.Empty;

    public CarBrand ToCarBrand(CarBrandViewModel carBrand)
    {
        return new CarBrand()
        {
            brandName = carBrand.brandName,
            logoUrl = carBrand.logoUrl
        };
    }
}