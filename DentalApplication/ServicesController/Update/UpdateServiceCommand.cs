using MediatR;

namespace DentalApplication.ServicesController.Update
{
    public class UpdateServiceCommand : IRequest<ServiceResponse>
    {
        public Guid? service_id { get; set; }
        public decimal? new_price { get; set; }
        public string? new_name { get; set; }
        public string? new_description { get; set; }
        public int? new_duration { get; set; }
        public List<Guid>? doctors { get; set; }
    }
}
