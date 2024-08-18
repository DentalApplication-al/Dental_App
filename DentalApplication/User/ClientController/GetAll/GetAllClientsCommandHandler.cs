using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.GetAll
{
    public class GetAllClientsCommandHandler : IRequestHandler<GetAllClientCommand, List<ClientListResponse>>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientsCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<ClientListResponse>> Handle(GetAllClientCommand request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAllClinicCLients(request.clinic_id);

            return clients;
        }
    }
}
