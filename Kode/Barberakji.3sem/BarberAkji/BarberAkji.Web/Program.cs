var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Gør det muligt at bruge MVC med views og controllere

builder.Services.AddHttpClient("BarberApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7051/api/"); // Dette skal matche den faste port
});

var app = builder.Build();

if (!app.Environment.IsDevelopment()) // Fejlhåndtering og HTTPS i production
{
    app.UseExceptionHandler("/Home/Error"); // Viser brugervenlig fejlside
    app.UseHsts(); // Tvinger HTTPS
}

app.UseHttpsRedirection(); // Sørger for at alt bliver kørt over HTTPS
app.UseStaticFiles(); // Mulighed for at servere fx billeder, CSS osv.

app.UseRouting(); // Aktiverer routing til controllers
app.UseAuthorization(); // Klar til hvis man vil bruge roller/autorisering

// Standardrute: starter i HomeController og Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Starter webapplikationen
