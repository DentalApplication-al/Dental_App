using DentalApplication.Swagger;
using DentalContracts.UserContracts.ClientContracts;
using MediatR;

namespace DentalApplication.User.ClientController.Update
{
    public class UpdateClientCommand : AddClientCommand, IRequest<Guid>
    {
        [SwaggerIgnore]
        public Guid? clientId { get; set; }
    }
}
