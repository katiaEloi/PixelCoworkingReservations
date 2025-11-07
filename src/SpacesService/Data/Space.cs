namespace SpacesService.Data
{
    public class Space
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public bool IsPrivate { get; set; }
    }
}
