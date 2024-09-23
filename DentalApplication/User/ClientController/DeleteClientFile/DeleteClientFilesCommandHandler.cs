using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using MediatR;

namespace DentalApplication.User.ClientController.DeleteClientFile
{
    public class DeleteClientFilesCommandHandler : IRequestHandler<DeleteClientFileCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly IBlobStorage _blobStorage;
        public DeleteClientFilesCommandHandler(IClientRepository clientRepository, IFilesRepository filesRepository, IBlobStorage blobStorage)
        {
            _clientRepository = clientRepository;
            _filesRepository = filesRepository;
            _blobStorage = blobStorage;
        }

        public async Task Handle(DeleteClientFileCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.clinic_id.Value, request.clientId) ??
                throw new NotFoundException("Client could not be found");

            var filesToDelete = client.CLientFiles
                .Where(a => request.files.Contains(a.Id))
                .ToList();

            var pathsToRemoveFromAzure = filesToDelete.Select(a => a.AbsolutePath).ToList();

            var areDeleted = await _filesRepository.DeleteAsync(filesToDelete);
            
            if (areDeleted)
            {
                await _blobStorage.DeleteBlobAsync(pathsToRemoveFromAzure);
            }
        }
    }
}
