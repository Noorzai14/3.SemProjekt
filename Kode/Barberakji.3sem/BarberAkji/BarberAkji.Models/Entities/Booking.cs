using System.ComponentModel.DataAnnotations;

namespace BarberAkji.Models.Entities
{
    public class Booking // Repræsenterer en booking i systemet
    {
        public int Id { get; set; } // Primær nøgle

        [Required] //required felt sørger for at sikre at feltet altid er udfyldt 
        public string CustomerName { get; set; } = string.Empty; // Navn på kunden

        [Required]
        public DateTime BookingDate { get; set; } // Dato og tidspunkt for bookingen

        public string? Note { get; set; } // Evt. note skrevet af kunden eller medarbejderen

        public int EmployeeId { get; set; } // Fremmednøgle til den ansatte
        public Employee Employee { get; set; } = default!; // Navigation til medarbejder

        public int ServiceId { get; set; } // Fremmednøgle til servicen
        public Service Service { get; set; } = default!; // Navigation til service
    }
}
