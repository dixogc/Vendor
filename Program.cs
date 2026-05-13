using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Text;
using System.Text.Json.Serialization;
using Vendor.Repository;
using Vendor.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

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

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<VentaRepository>();
builder.Services.AddScoped<RegistroVentaRepository>();
builder.Services.AddScoped<InversionRepository>();
builder.Services.AddScoped<DetalleInversionRepository>();
builder.Services.AddScoped<MovimientoRepository>();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<InversionService>();
builder.Services.AddScoped<MovimientoService>();
builder.Services.AddScoped<TokenService>(); 


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
