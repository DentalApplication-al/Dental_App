using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.GetAllFiles
{
    public class GetAllClientFilesCommandHandler : IRequestHandler<GetAllClientFilesCommand, List<FileResponse>>
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IBlobStorage _blobStorage;
        public GetAllClientFilesCommandHandler(
            IFilesRepository filesRepository, 
            IBlobStorage blobStorage)
        {
            _filesRepository = filesRepository;
            _blobStorage = blobStorage;
        }

        public async Task<List<FileResponse>> Handle(GetAllClientFilesCommand request, CancellationToken cancellationToken)
        {
            var clientFiles = await _filesRepository.GetClientFiles(request.id, request.clinic_id.Value, request.images);
            var files = clientFiles.Select(x => new FileResponse
            {
                id = x.Id,
                name = x.Name,
                size = x.Size,
                unit = x.Unit,
                link = _blobStorage.GetLink(x.AbsolutePath, null),
                uploaded_date = x.CreatedOn.ToString("dd-MM-yyyy HH:mm")
            }).ToList();
            return files;
        }
    }
}
