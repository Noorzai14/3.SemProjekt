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

// CORS setup – nødvendigt for at kunne tilgå API'et fra Web-projektet eller Swagger UI i browseren
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Aktiver Swagger middleware kun i udviklingsmiljø
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();         // Gør swagger-json tilgængelig (kun i development)
    app.UseSwaggerUI();       // Swagger brugergrænseflade
}

// Aktiverer CORS-policy (vigtigt at den kommer før authorization og routing)
app.UseCors("AllowAll"); // Tillader alle domæner at tilgå API'et under udvikling

// Middleware til autorisation (kan tilpasses med policies hvis nødvendigt)
app.UseAuthorization(); // Autorisation (kan udvides med policies)
app.MapControllers();   // Mapper controller-endpoints (API routes)

app.Run(); // Starter applikationen
