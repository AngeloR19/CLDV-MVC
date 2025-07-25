namespace CLDVPOE25.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int EventItemId { get; set; }
        public EventItem? EventItem { get; set; }
        public int VenueId { get; set; }
        public Venue? Venue { get; set; }
        public DateTime DateOfEvent { get; set; }
        public int Duration { get; set; }
        public string Feedback { get; set; }
    }
}
