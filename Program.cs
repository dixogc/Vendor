using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text.Json.Serialization;
using Vendor.Repository;
using Vendor.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cadena de conexión
var stringDeConexion = builder.Configuration.GetConnectionString("Default");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<VendorDbContext>(options =>
    options.UseNpgsql(stringDeConexion));

builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<VentaRepository>();
builder.Services.AddScoped<RegistroVentaRepository>();
builder.Services.AddScoped<VentaService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
