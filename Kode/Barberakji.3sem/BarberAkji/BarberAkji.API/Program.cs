using BarberAkji.API.Data;
using BarberAkji.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Tilføjer controller-understøttelse

// Swagger til API-dokumentation (automatisk genereret UI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection af Dapper context og repository
builder.Services.AddSingleton<DapperContext>();  // Registrerer DapperContext så vi kan oprette SQL-forbindelser i vores repository
builder.Services.AddScoped<BookingRepository>(); // Dependency injection af vores repository

var app = builder.Build();

// Aktiver Swagger middleware kun i udviklingsmiljø
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();         // Gør swagger-json tilgængelig (kun i development)
    app.UseSwaggerUI();       // Swagger brugergrænseflade
}

// Middleware til autorisation (kan tilpasses med policies hvis nødvendigt)
app.UseAuthorization(); // Autorisation (kan udvides med policies)
app.MapControllers();   // Mapper controller-endpoints (API routes)

app.Run(); // Starter applikationen
