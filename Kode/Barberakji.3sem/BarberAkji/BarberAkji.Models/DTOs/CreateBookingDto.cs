namespace BarberAkji.Models.DTOs
{
    public class CreateBookingDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string? Note { get; set; }
    }
}