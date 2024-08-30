using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using MediatR;

namespace DentalApplication.User.ClientController.Delete
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IClientRepository _clientRepository;

        public DeleteClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.clinic_id.Value, request.id.Value) ??
                throw new NotFoundException("The client could not be found.");

            var isDeleted = await _clientRepository.DeleteAsync(client);

            if (!isDeleted)
            {
                throw new NotDeletedException("The client could not be deleted.");
            }
        }
    }
}
