using System.ComponentModel.DataAnnotations;
using BarberAkji.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarberAkji.Models
{
    public class BookingViewModel
    {
        [Required]
        public string CustomerName { get; set; } = "";

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public string? Note { get; set; }

        public List<SelectListItem> Employees { get; set; } = new();
        public List<SelectListItem> Services { get; set; } = new();
    }
}
