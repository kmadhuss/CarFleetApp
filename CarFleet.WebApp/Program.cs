using CarFleet.Data.Data;
using CarFleet.Data.Models;
using CarFleet.Data.Repository;
using CarFleet.Service.Services;
using CarFleet.WebApp.ExceptionHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationModel.connectionString = builder.Configuration.GetConnectionString("connectionString");
ConfigurationModel.jwtKey = builder.Configuration.GetValue<string>("ServiceConfiguration:JwtSettings:Secret");

builder.Services.AddDbContext<DatabaseContext>();

InjectDL(builder.Services);
builder.Services.AddTransient<ExceptionHandler>();
var JwtSecretkey = Encoding.ASCII.GetBytes(ConfigurationModel.jwtKey);
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(JwtSecretkey),
    ValidateIssuer = false,
    ValidateAudience = false,
    RequireExpirationTime = false,
    ValidateLifetime = true
};
builder.Services.AddSingleton(tokenValidationParameters);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = tokenValidationParameters;
});

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader()
 );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandler>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void InjectDL(IServiceCollection services)
{
    services.AddScoped<ICarRepository, CarRepository>();
    services.AddScoped<ICarBrandRepository, CarBrandRepository>();
    services.AddScoped<ICarService, CarService>();
    services.AddScoped<ICarBrandService, CarBrandService>();
    services.AddScoped<IAuthService, AuthService>();
}