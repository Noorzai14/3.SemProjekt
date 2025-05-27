using BarberAkji.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using BarberAkji.Models.Entities;

namespace BarberAkji.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingRepository _bookingRepo;

    public BookingController(BookingRepository bookingRepo)
    {
        _bookingRepo = bookingRepo;
    }

    [HttpPost]
    public async Task<IActionResult> PostBooking([FromBody] Booking dto)
    {
        var result = await _bookingRepo.TryCreateBookingAsync(dto);
        if (!result.isSuccess)
            return Conflict(result.message);

        return Ok("Booking oprettet");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await _bookingRepo.GetAllBookingsAsync();
        return Ok(bookings);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var deleted = await _bookingRepo.DeleteBookingAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
