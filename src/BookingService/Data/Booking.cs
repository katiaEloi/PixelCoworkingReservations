namespace BookingService.Data
{
    public class Booking
    {
        public int Id { get; set; }
        public int SpaceId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
