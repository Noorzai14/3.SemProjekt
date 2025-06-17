using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BarberAkji.Models.Entities
{
    public class Booking // Repræsenterer en booking i systemet
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } // Primær nøgle

        [Required] //required felt sørger for at sikre at feltet altid er udfyldt 
        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty; // Navn på kunden

        [Required]
        [JsonPropertyName("bookingDate")]
        public DateTime BookingDate { get; set; } // Dato og tidspunkt for bookingen

        [JsonPropertyName("note")]
        public string? Note { get; set; } // Evt. note skrevet af kunden eller medarbejderen

        [JsonPropertyName("employeeId")]
        public int EmployeeId { get; set; } // Fremmednøgle til den ansatte
        [JsonPropertyName("employee")]
        public Employee? Employee { get; set; } = default!; // Navigation til medarbejder
        [JsonPropertyName("serviceId")]
        public int ServiceId { get; set; } // Fremmednøgle til servicen
        [JsonPropertyName("service")]
        public Service? Service { get; set; } = default!; // Navigation til service
    }
}
