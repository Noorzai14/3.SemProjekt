using System.ComponentModel.DataAnnotations;

namespace BarberAkji.Web.Models
{
    public class BookingViewModel // ViewModel bruges til at vise/formdata i Web-delen – adskilt fra databasen
    {
        [Required] // Brugerens navn er påkrævet
        public string CustomerName { get; set; } = string.Empty;

        [Required] // Dato og tid for bookingen – også påkrævet
        public DateTime BookingDate { get; set; }

        public string? Note { get; set; } // Valgfri note – fx hvis kunden har en speciel besked
    }
}
