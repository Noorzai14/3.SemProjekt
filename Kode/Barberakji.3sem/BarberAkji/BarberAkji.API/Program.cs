using BarberAkji.API.Data;
using BarberAkji.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Tilf�jer controller-underst�ttelse

// Swagger til API-dokumentation (automatisk genereret UI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection af Dapper context og repository
builder.Services.AddSingleton<DapperContext>();  // Registrerer DapperContext s� vi kan oprette SQL-forbindelser i vores repository
builder.Services.AddScoped<BookingRepository>(); // Dependency injection af vores repository

var app = builder.Build();

// Aktiver Swagger middleware kun i udviklingsmilj�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();         // G�r swagger-json tilg�ngelig (kun i development)
    app.UseSwaggerUI();       // Swagger brugergr�nseflade
}

// Middleware til autorisation (kan tilpasses med policies hvis n�dvendigt)
app.UseAuthorization(); // Autorisation (kan udvides med policies)
app.MapControllers();   // Mapper controller-endpoints (API routes)

app.Run(); // Starter applikationen
