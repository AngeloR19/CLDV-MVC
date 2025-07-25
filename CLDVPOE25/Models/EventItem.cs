namespace CLDVPOE25.Models
{
    public class EventItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int EventTypeId { get; set; }
        public EventType? EventType { get; set; }


        public DateTime DateOfEvent { get; set; }
        public string ContactEmail { get; set; }

        public int? VenueId { get; set; }  // Nullable to allow no venue initially
        public Venue Venue { get; set; }

        public List<Booking> Booking { get; set; } = new();
    }
}

