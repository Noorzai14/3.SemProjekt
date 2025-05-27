using BarberAkji.Models.Entities;
using BarberAkji.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace BarberAkji.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateBooking()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("BarberApi"); // husk navngivningen i Program.cs
            var response = await client.PostAsJsonAsync("booking", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Booking oprettet!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Fejl ved oprettelse af booking.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Calendar()
        {
            var client = _httpClientFactory.CreateClient("BarberApi");
            var response = await client.GetAsync("booking");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Kunne ikke hente bookinger.";
                return View(new List<Booking>());
            }

            var bookings = await response.Content.ReadFromJsonAsync<List<Booking>>();
            return View(bookings);
        }
    }
}
