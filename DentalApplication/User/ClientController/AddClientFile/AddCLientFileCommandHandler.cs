using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalDomain.Files;
using MediatR;

namespace DentalApplication.User.ClientController.AddClientFile
{
    public class AddCLientFileCommandHandler : IRequestHandler<AddClientFileCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly IBlobStorage _blobStorage;
        public AddCLientFileCommandHandler(IClientRepository clientRepository, IFilesRepository filesRepository, IBlobStorage blobStorage)
        {
            _clientRepository = clientRepository;
            _filesRepository = filesRepository;
            _blobStorage = blobStorage;
        }

        public async Task Handle(AddClientFileCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.clinic_id.Value, request.clientId) ??
                throw new NotFoundException("Client could not be found.");

            List<Documents> files = [];
            List<string> paths = [];
            foreach (var item in request.files)
            {
                var upload = await _blobStorage.Upload(item);

                if (upload.hasSucceded)
                {
                    paths.Add(upload.data);
                }
                else
                {
                    await _blobStorage.DeleteBlobAsync(paths);
                    throw new ServerError("Files could not be uploaded.");
                }
                Documents file = new Documents
                {
                    ClientId = client.Id,
                    ClinicId = request.clinic_id.Value,
                    Name = item.FileName,
                    Size = ConvertBytesToReadableSize(item.Length).Item2,
                    Unit = ConvertBytesToReadableSize(item.Length).Item1,
                    AbsolutePath = upload.data,
                    RelativePath = upload.data,
                    CreatedOn = DateTime.UtcNow,
                };
                files.Add(file);

            }
            var isUploadded = await _filesRepository.AddRange(files);
            if (!isUploadded)
            {
                await _blobStorage.DeleteBlobAsync(paths);
                throw new ServerError("Files could not be uploaded.");
            }

        }
        public (string, int) ConvertBytesToReadableSize(long bytes)
        {
            
            if (bytes >= 1_000_000) 
            {
                return ("mb", (int)(bytes / 1_000_000));
            }
            else if (bytes >= 1_000)
            {
                return ("kb", (int)(bytes / 1_000));
            }
            else 
            {
                return ("kb", 1);
            }
        }
    }
}
