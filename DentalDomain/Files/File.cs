namespace DentalDomain.Files
{
    public class File
    {
        public Guid Id { get; set; }
        public string RelativePath { get; set; }
        public string AbsolutePath { get; set; }
        public Guid EntityId { get; set; }
    }
}
