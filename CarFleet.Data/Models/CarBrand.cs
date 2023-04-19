using System.ComponentModel.DataAnnotations;

namespace CarFleet.Data.Models;

public class CarBrand
{
    [Key]
    public Guid id { get; set; }

    public string brandName { get; set; } = string.Empty;
    public string logoUrl { get; set; } = string.Empty;
}