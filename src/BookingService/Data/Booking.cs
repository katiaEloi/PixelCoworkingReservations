
namespace BookingService.Data
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
    public class Booking
    {
        public int Id { get; set; }
        public int SpaceId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = BookingStatus.Pending.ToString();
    }
}
