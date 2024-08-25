using DentalApplication.Common;
using DentalApplication.ServicesController.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.ServicesController.Update
{
    public class UpdateServiceCommand : CommandBase, IRequest<ServiceResponse>
    {
        public Guid? id { get; set; }
        public decimal? price { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? duration { get; set; }
        public List<Guid>? doctors { get; set; }
    }
}
