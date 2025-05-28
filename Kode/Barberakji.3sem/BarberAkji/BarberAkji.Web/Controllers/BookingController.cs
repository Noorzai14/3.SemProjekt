using Microsoft.AspNetCore.Mvc;
using BarberAkji.Models;
using BarberAkji.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace BarberAkji.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory) // Vi bruger IHttpClientFactory for at kunne kalde API'en via en navngiven client
        { 
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index() => RedirectToAction("Create"); // Når man går ind på /Booking, så redirecter vi til Create

        [HttpGet] // Viser bookingformularen og loader data til dropdowns
        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient("BarberApi");

            // Henter data til medarbejdere og services fra API
            var employeesResponse = await client.GetAsync("employee");
            var servicesResponse = await client.GetAsync("service");

            if (!employeesResponse.IsSuccessStatusCode || !servicesResponse.IsSuccessStatusCode) // Hvis noget fejler, så vis fejlvisning
                return View("Error");

            // Parser JSON til lister af objekter
            var employees = await employeesResponse.Content.ReadFromJsonAsync<List<Employee>>();
            var services = await servicesResponse.Content.ReadFromJsonAsync<List<Service>>();

            var model = new BookingViewModel // Sætter Employees og Services ind i viewmodel som dropdowns
            {
                Employees = employees.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList(),

                Services = services.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost] // Når formularen postes, tjekker vi om model er gyldig og sender booking til API
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (!ModelState.IsValid) // Hvis noget mangler eller er ugyldigt, så reloades formen med fejl
                return await ReloadForm(model);

            var client = _httpClientFactory.CreateClient("BarberApi");

            var booking = new Booking // Vi laver en Booking-objekt ud fra ViewModel og sender det som JSON
            {
                CustomerName = model.CustomerName,
                BookingDate = model.BookingDate,
                Note = model.Note,
                EmployeeId = model.EmployeeId,
                ServiceId = model.ServiceId
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(booking), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("booking", jsonContent);

            if (!response.IsSuccessStatusCode) // Hvis bookingen overlapper en eksisterende, så vis fejlbesked
            {
                ModelState.AddModelError("", "Tiden overlapper med en eksisterende booking.");
                return await ReloadForm(model);
            }

            // Hvis alt går godt, så redirect til forsiden og vis succesbesked
            TempData["Success"] = "Booking oprettet!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet] // Viser kalenderoversigten med alle bookinger
        public async Task<IActionResult> Calendar()
        {
            var client = _httpClientFactory.CreateClient("BarberApi");
            var response = await client.GetAsync("booking");

            if (!response.IsSuccessStatusCode)
                return View("Error");

            var bookings = await response.Content.ReadFromJsonAsync<List<Booking>>();
            return View(bookings);
        }

        [HttpPost] // Denne metode er en placeholder – kræver endpoint i API'en for at fungere
        public IActionResult DeleteAll()
        {
            // Hvis du vil lave "slet alle" funktion via API, lav endpoint i API'en
            TempData["Info"] = "DeleteAll skal laves i API først.";
            return RedirectToAction("Calendar");
        }

        // Bruges til at genindlæse medarbejdere og services hvis form fejler
        private async Task<IActionResult> ReloadForm(BookingViewModel model) 
        {
            var client = _httpClientFactory.CreateClient("BarberApi");

            var employeesResponse = await client.GetAsync("employee");
            var servicesResponse = await client.GetAsync("service");

            if (!employeesResponse.IsSuccessStatusCode || !servicesResponse.IsSuccessStatusCode)
                return View("Error");

            var employees = await employeesResponse.Content.ReadFromJsonAsync<List<Employee>>();
            var services = await servicesResponse.Content.ReadFromJsonAsync<List<Service>>();

            model.Employees = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();

            model.Services = services.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            return View("Create", model);
        }
    }
}
