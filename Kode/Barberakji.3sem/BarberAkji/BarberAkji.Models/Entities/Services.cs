namespace BarberAkji.Models.Entities
{
    public class Service // Repræsenterer en service i barbersalonen (fx klipning, trimning)
    {
        public int Id { get; set; } // Primær nøgle for service

        public string Name { get; set; } = string.Empty; // Navnet på servicen

        public int DurationInMinutes { get; set; } // Hvor lang tid servicen tager

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Bookinger hvor servicen er brugt
    }
}
