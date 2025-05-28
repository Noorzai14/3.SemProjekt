namespace BarberAkji.Models.Entities
{
    public class Employee // Repræsenterer en medarbejder i barbershoppen
    {
        public int Id { get; set; } // Primær nøgle
        public string Name { get; set; } = string.Empty; // Medarbejderens navn

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Liste over bookinger tilknyttet denne medarbejder
    }
}
