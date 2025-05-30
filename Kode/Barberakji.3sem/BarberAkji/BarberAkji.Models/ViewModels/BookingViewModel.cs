using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarberAkji.ViewModels
{
    public class BookingViewModel
    {
        [Required]
        [Display(Name = "Navn")]
        public string CustomerName { get; set; } = "";

        [Required]
        [Display(Name = "Booking Dato og Tid")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Barber")]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        public string? Note { get; set; }

        public List<SelectListItem> Employees { get; set; } = new();
        public List<SelectListItem> Services { get; set; } = new();
    }
}