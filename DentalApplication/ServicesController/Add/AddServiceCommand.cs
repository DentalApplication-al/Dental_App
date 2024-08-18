using DentalApplication.Common;
using MediatR;

namespace DentalApplication.ServicesController.Add
{
    public class AddServiceCommand : CommandBase, IRequest<ServiceResponse>
    {
        public decimal? price { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? duration { get; set; }
        public List<Guid>? doctors { get; set; }
    }
}
