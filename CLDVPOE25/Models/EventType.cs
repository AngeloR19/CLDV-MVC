namespace CLDVPOE25.Models
{
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for related events
        public List<EventItem> EventItems { get; set; } = new();
    }
}
