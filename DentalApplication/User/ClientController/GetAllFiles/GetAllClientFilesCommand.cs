using DentalApplication.Common;
using DentalApplication.User.ClientController.DTO;
using MediatR;

namespace DentalApplication.User.ClientController.GetAllFiles
{
    public class GetAllClientFilesCommand : CommandBase, IRequest<List<FileResponse>>
    {
        public Guid id { get; set; }
        public bool images { get; set; }
    }
}
