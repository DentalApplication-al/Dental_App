using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.Update
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;

        public UpdateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientResponse> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.client_id.Value, request.clinic_id.Value);

            client.Update(
                request.new_first_name,
                request.new_last_name,
                request.new_phone,
                request.new_email,
                request.new_birthday.Value
            );

            await _clientRepository.UpdateAsync(client);

            return ClientResponse.Map(client);
        }
    }
}
