using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.GetAll
{
    public class GetAllClientsCommandHandler : IRequestHandler<GetAllClientCommand, PaginatedResponse<ListClient>>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientsCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<PaginatedResponse<ListClient>> Handle(GetAllClientCommand request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetPaginatedClients(request.clinic_id.Value, request.take, request.page, request.search);

            return clients;
        }
    }
}
