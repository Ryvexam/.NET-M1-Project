namespace Shared
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int MaxRegistrations { get; set; }
        public EventStatus Status { get; set; }
        public int? MinimumAge { get; set; }
        public List<string>? Tags { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

    }

    public enum EventStatus
    {
        Draft,       // Brouillon
        Published,   // Publié
        Cancelled   // Annulé
    }
}