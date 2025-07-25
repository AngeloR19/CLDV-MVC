namespace CLDVPOE25.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public int PricePerHour { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; }

        public List<Booking> Booking { get; set; } = new();
    }
}

