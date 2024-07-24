using DentalApplication.Common.Interfaces.IRepositories;
using DentalContracts.UserContracts.ClientContracts;
using DentalDomain.Users.Clients;
using MediatR;

namespace DentalApplication.User.ClientController.Add
{
    public class AddClientCommmandHandler : IRequestHandler<AddClientCommand>
    {
        private readonly IClientRepository _clientRepository;

        public AddClientCommmandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var client = Client.Create(
                request.birthday,
                request.first_name,
                request.last_name,
                request.email,
                request.phone);

            await _clientRepository.AddAsync(client);

        }
    }
}
