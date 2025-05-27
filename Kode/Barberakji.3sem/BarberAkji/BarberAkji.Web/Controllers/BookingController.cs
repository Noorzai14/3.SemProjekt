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

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index() => RedirectToAction("Create");

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient("BarberApi");

            var employeesResponse = await client.GetAsync("employee");
            var servicesResponse = await client.GetAsync("service");

            if (!employeesResponse.IsSuccessStatusCode || !servicesResponse.IsSuccessStatusCode)
                return View("Error");

            var employees = await employeesResponse.Content.ReadFromJsonAsync<List<Employee>>();
            var services = await servicesResponse.Content.ReadFromJsonAsync<List<Service>>();

            var model = new BookingViewModel
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

        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return await ReloadForm(model);

            var client = _httpClientFactory.CreateClient("BarberApi");

            var booking = new Booking
            {
                CustomerName = model.CustomerName,
                BookingDate = model.BookingDate,
                Note = model.Note,
                EmployeeId = model.EmployeeId,
                ServiceId = model.ServiceId
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(booking), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("booking", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Tiden overlapper med en eksisterende booking.");
                return await ReloadForm(model);
            }

            TempData["Success"] = "Booking oprettet!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Calendar()
        {
            var client = _httpClientFactory.CreateClient("BarberApi");
            var response = await client.GetAsync("booking");

            if (!response.IsSuccessStatusCode)
                return View("Error");

            var bookings = await response.Content.ReadFromJsonAsync<List<Booking>>();
            return View(bookings);
        }

        [HttpPost]
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
