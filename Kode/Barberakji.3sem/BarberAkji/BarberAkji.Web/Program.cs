var builder = WebApplication.CreateBuilder(args);

// Tilføj MVC (controllers og views)
builder.Services.AddControllersWithViews();

// Registrer HttpClient til kald mod API
builder.Services.AddHttpClient("BarberApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7051/api/"); // Tilpas hvis din API-adresse ændres
});

var app = builder.Build();

// Middleware-konfiguration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
