using DentalDomain.Services;

namespace DentalApplication.ServicesController.DTO
{
    public class ListService
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public List<string> doctors { get; set; } = new();

        public static ListService Map(Service service)
        {
            return new ListService
            {
                id = service.Id,
                name = service.Name,
                price = service.Price
            };
        }
    }
}
