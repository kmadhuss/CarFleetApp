using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarFleet.Data.Declaration.Enums;

namespace CarFleet.Data.Models;

public class Car
{
    [Key]
    public Guid id { get; set; }

    public string modelName { get; set; } = string.Empty;
    public string colorName { get; set; } = string.Empty;
    public string colorCode { get; set; } = string.Empty;
    public DateTime launchDate { get; set; }
    public DateTime releaseDate { get; set; }
    public FuelType fuelType { get; set; }
    public TransmissionType transmissionType { get; set; }
    public DateTime createdDate { get; set; }
    public DateTime ModifiedDate { get; set; }

    [ForeignKey("CarBrand")]
    public Guid brandId { get; set; }

    public virtual CarBrand CarBrand { get; set; } = default!;

}