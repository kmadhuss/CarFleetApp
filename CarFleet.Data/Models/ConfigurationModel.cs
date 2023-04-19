namespace CarFleet.Data.Models;

public class ConfigurationModel
{
    public static string jwtKey { get; set; } = string.Empty;
    public static string connectionString { get; set; } = string.Empty;
}