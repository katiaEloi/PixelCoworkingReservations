namespace BookingService.Dto
{
    public class SpaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public bool IsPrivate { get; set; }
    }

    public class BookingResponse
    {
        public string Message { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
