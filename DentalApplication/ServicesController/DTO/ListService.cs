namespace DentalApplication.ServicesController.DTO
{
    public class ListService
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public List<string> doctors { get; set; } = new();
    }
}
