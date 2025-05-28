using System.ComponentModel.DataAnnotations;
using BarberAkji.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarberAkji.Models
{
    public class BookingViewModel
    {
        [Required]
        public string CustomerName { get; set; } = ""; // Navn på kunden (skal udfyldes)

        [Required]
        public DateTime BookingDate { get; set; } // Hvornår bookingen skal finde sted

        [Required]
        public int EmployeeId { get; set; } // Hvilken medarbejder der er valgt

        [Required]
        public int ServiceId { get; set; } // Hvilken service kunden har valgt

        public string? Note { get; set; } // Mulighed for at skrive en note til bookingen

        public List<SelectListItem> Employees { get; set; } = new(); // Bruges til dropdown med medarbejdere
        public List<SelectListItem> Services { get; set; } = new(); // Bruges til dropdown med services
    }
}
