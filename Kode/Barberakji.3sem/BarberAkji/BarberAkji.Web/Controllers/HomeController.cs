using BarberAkji.Models.Entities;
using BarberAkji.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace BarberAkji.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory) // Vi bruger IHttpClientFactory til at oprette HTTP-klienter, så vi kan snakke med vores API
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet] // Viser forsiden (Index view)
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet] // Viser formularen til at oprette en ny booking
        public IActionResult CreateBooking()
        {
            return View();
        }

        [HttpPost] // Håndterer når brugeren indsender bookingformularen
        public async Task<IActionResult> CreateBooking(Booking model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("BarberApi"); // husk navngivningen i Program.cs
            var response = await client.PostAsJsonAsync("booking", model); // Sender booking til API'en

            if (response.IsSuccessStatusCode) // Hvis alt går godt, vis besked og send tilbage til forsiden
            {
                TempData["Success"] = "Booking oprettet!";
                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", "Fejl ved oprettelse af booking."); // Hvis der er fejl, vis fejlbesked og behold data i formularen
            return View(model);
        }

        [HttpGet] // Viser kalenderen med alle bookinger
        public async Task<IActionResult> Calendar()
        {
            var client = _httpClientFactory.CreateClient("BarberApi");
            var response = await client.GetAsync("booking"); // Henter bookinger fra API

            if (!response.IsSuccessStatusCode) // Hvis det fejler, send tom liste og fejlbesked til view
            {
                TempData["Error"] = "Kunne ikke hente bookinger.";
                return View(new List<Booking>());
            }

            var bookings = await response.Content.ReadFromJsonAsync<List<Booking>>(); // Læser JSON-data til en liste af Booking-objekter
            return View(bookings);
        }
    }
}
