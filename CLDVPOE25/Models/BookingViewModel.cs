namespace CLDVPOE25.Models
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public DateTime EventDate { get; set; }
        public string VenueName { get; set; }
        public string VenueLocation { get; set; }
        public int VenueCapacity { get; set; }
        public decimal VenuePricePerHour { get; set; }
        public DateTime BookingDate { get; set; }
        public int Duration { get; set; }
        public string Feedback { get; set; }
    }
}
