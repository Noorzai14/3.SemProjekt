using System.ComponentModel.DataAnnotations;

namespace BarberAkji.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public DateTime BookingDate { get; set; }

        public string? Note { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = default!;
    }
}
