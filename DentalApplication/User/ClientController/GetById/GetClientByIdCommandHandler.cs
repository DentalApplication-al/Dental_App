using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.GetById
{
    public class GetClientByIdCommandHandler : IRequestHandler<GetClientByIdCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByIdCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientResponse> Handle(GetClientByIdCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.client_id.Value, request.clinic_id) ??
                throw new NotFoundException("The client could not be found");
            
            return ClientResponse.Map(client);
        }
    }
}
