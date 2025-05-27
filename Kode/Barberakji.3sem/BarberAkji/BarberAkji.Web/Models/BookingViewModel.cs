using System.ComponentModel.DataAnnotations;

namespace BarberAkji.Web.Models
{
    public class BookingViewModel
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public DateTime BookingDate { get; set; }

        public string? Note { get; set; }
    }
}
