namespace BarberAkji.Models.Entities
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int DurationInMinutes { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
