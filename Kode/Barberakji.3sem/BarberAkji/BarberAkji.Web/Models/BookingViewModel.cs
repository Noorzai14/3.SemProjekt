using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarberAkji.Models

{
    public class BookingViewModel
    {
        [Required]
        [Display(Name = "Navn")]
        public required string CustomerName { get; set; }

        [Required]
        [Display(Name = "Booking Dato og Tid")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; } = DateTime.Now; // Sætter startdato til nu

        public string? Note { get; set; }

        [Required]
        [Display(Name = "Barber")]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        // Drop-down lister til ansatte og services
        public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Services { get; set; } = new List<SelectListItem>();
    }
}
