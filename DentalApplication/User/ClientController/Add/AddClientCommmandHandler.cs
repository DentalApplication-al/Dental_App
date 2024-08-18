using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.ClientController.DTO;
using DentalContracts.UserContracts.ClientContracts;
using DentalDomain.Users.Clients;
using MediatR;

namespace DentalApplication.User.ClientController.Add
{
    public class AddClientCommmandHandler : IRequestHandler<AddClientCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;

        public AddClientCommmandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientResponse> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var client = Client.Create(
                request.birthday.Value,
                request.first_name,
                request.last_name,
                request.email,
                request.phone,
                request.clinic_id);

            await _clientRepository.AddAsync(client);

            await _clientRepository.SaveChangesAsync();
            return ClientResponse.Map(client);  

        }
    }
}
