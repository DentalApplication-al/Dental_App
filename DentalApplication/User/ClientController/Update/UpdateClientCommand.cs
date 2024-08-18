using DentalApplication.Common;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.Update
{
    public class UpdateClientCommand : CommandBase, IRequest<ClientResponse>
    {
        public Guid? client_id { get; set; }
        public string? new_first_name { get; set; }
        public string? new_last_name { get; set; }
        public string? new_email { get; set; }
        public string? new_phone { get; set; }
        public DateTime? new_birthday { get; set; }
    }
}
