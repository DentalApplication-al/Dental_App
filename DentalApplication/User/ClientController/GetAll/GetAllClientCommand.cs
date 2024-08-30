using DentalApplication.Common;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.GetAll
{
    public class GetAllClientCommand : CommandBase, IRequest<PaginatedResponse<ListClient>>
    {
        public int page { get; set; }
        public int take { get; set; }
        public string? search { get; set; }
    }
}
