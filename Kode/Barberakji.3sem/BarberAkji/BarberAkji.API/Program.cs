using BarberAkji.API.Data;
using BarberAkji.API.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(); // Tilf�jer controller-underst�ttelse

// Swagger til API-dokumentation
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DapperContext>();  // Registrerer DapperContext s� vi kan oprette SQL-forbindelser i vores repository
builder.Services.AddScoped<BookingRepository>(); // Dependency injection af vores repository



var app = builder.Build();


// Aktiver Swagger middleware (kun i udvikling)
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization(); // Autorisation (kan udvides med policies)
app.MapControllers(); // Mapper controller-endpoints (API routes)

app.Run(); // Starter applikationen
