using DentalApplication.Common;
using DentalApplication.ServicesController.DTO;
using MediatR;

namespace DentalApplication.ServicesController.Get
{
    public class GetAllServicesCommand : CommandBase, IRequest<PaginatedResponse<ListService>>
    {
        public int page { get; set; }
        public int take { get; set; }
        public string? search { get; set; }
    }
}
