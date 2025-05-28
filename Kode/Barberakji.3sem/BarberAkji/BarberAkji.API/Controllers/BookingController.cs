using BarberAkji.API.Repositories; // Importerer repository-klassen til booking
using Microsoft.AspNetCore.Mvc; // Indeholder funktionalitet til at lave en API-controller
using BarberAkji.Models.Entities; // Importerer Booking-modellen

namespace BarberAkji.API.Controllers;

[ApiController] // Angiver at denne klasse er en API-controller og automatisk håndterer HTTP-anmodninger
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingRepository _bookingRepo; // Repository-objektet som bruges til at udføre databaseoperationer

    public BookingController(BookingRepository bookingRepo) // Constructor der bruger dependency injection til at hente BookingRepository
    {
        _bookingRepo = bookingRepo;
    }

    [HttpPost] // POST-endpoint til at oprette en ny booking
    public async Task<IActionResult> PostBooking([FromBody] Booking dto)
    {
        var result = await _bookingRepo.TryCreateBookingAsync(dto);  // Kalder metoden i repository'et og forsøger at oprette en booking
        if (!result.isSuccess) // Hvis det ikke lykkedes (fx pga. dobbeltbooking), send en conflict
            return Conflict(result.message);

        return Ok("Booking oprettet"); // Hvis det lykkedes, send en OK-status
    }

    [HttpGet] // GET-endpoint til at hente alle bookinger fra databasen
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await _bookingRepo.GetAllBookingsAsync(); // Henter bookinger fra repository
        return Ok(bookings);// Returnerer bookingerne som JSONv
    }

    [HttpDelete("{id}")] // DELETE-endpoint til at slette en booking baseret på ID
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var deleted = await _bookingRepo.DeleteBookingAsync(id); // Kalder repository for at forsøge at slette bookingen
        if (!deleted) // Hvis der ikke blev fundet en booking med det ID, returner 404
            return NotFound();

        return NoContent(); // Hvis det lykkedes, returner 204 (no content)
    }
}
