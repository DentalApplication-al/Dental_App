using DentalApplication.Common;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalContracts.UserContracts.ClientContracts
{
    public class AddClientCommand : CommandBase, IRequest<Guid>
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public DateOnly? birthday { get; set; }
        public string? description { get; set; }

    }
}
