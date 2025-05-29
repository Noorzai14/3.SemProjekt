using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BarberAkji.Web.Models
{
    public class BookingViewModel // ViewModel bruges til at vise/formdata i Web-delen – adskilt fra databasen
    {
        [Required] // Brugerens navn er påkrævet
        public string CustomerName { get; set; } = string.Empty;

        [Required] // Dato og tid for bookingen – også påkrævet
        public DateTime BookingDate { get; set; }

        public string? Note { get; set; } // Valgfri note – fx hvis kunden har en speciel besked, allergener, promotions fra sociale medier osv. 

        [Required] // Ansat/barber er påkrævet
        public int EmployeeId { get; set; }

        [Required] // Service er påkrævet
        public int ServiceId { get; set; }

        // Dropdowns til at vise medarbejdere og services i viewet
        public List<SelectListItem>? Employees { get; set; }
        public List<SelectListItem>? Services { get; set; }
    }
}
