using Microsoft.EntityFrameworkCore;
using Transacciones.API.Data;
using Transacciones.API.Models.Entities;
using Transacciones.API.Repositories.Implementations;
using Transacciones.API.Repositories.Interfaces;
using Transacciones.API.Services.Implementations;
using Transacciones.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DbContext
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<TransaccionesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"),
    npgsqlOptions => npgsqlOptions.MapEnum<TipoTransaccion>("tipo_transaccion", "transacciones")));

// HttpClient para comunicacion con Productos.API
builder.Services.AddHttpClient<IProductoClienteService, ProductoClienteService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ProductosAPI:BaseUrl"]!);
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Repositorios y Servicios
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();
builder.Services.AddScoped<ITransaccionService, TransaccionService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();

app.Run();