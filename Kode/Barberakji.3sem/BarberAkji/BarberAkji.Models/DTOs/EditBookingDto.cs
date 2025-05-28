namespace BarberAkji.Models.DTOs
{
  
    public class EditBookingDto // DTO til redigering af eksisterende bookinger – bruges til at sende ændringer til API'et
    {
        public int Id { get; set; } // Bookingens ID, så vi ved hvilken der skal opdateres
        public string CustomerName { get; set; } = string.Empty; // Opdateret kundenavn
        public DateTime BookingDate { get; set; } // Ny dato/tidspunkt for bookingen
        public string? Note { get; set; } // Eventuel opdateret note
        public int EmployeeId { get; set; } // Opdateret medarbejdervalg
        public int ServiceId { get; set; } // Opdateret servicevalg
    }
}
