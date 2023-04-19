using CarFleet.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFleet.Data.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationModel.connectionString);
    }

    public DbSet<CarBrand> carBrands { get; set; }
    public DbSet<Car> cars { get; set; }
}