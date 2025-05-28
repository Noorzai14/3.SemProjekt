namespace BarberAkji.Models.DTOs
{
    public class CreateBookingDto // DTO til oprettelse af en booking – bruges til at sende data fra frontend til API
    {
        public string CustomerName { get; set; } = string.Empty; // Navn på kunden
        public DateTime BookingDate { get; set; } // Hvornår bookingen skal finde sted
        public string? Note { get; set; } // Valgfri kommentar fra kunden
    }
}