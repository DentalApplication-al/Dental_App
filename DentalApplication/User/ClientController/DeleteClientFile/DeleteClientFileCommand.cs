using DentalApplication.Common;
using MediatR;

namespace DentalApplication.User.ClientController.DeleteClientFile
{
    public class DeleteClientFileCommand : CommandBase, IRequest
    {
        public Guid clientId { get; set; }
        public List<Guid> files { get; set; }
    }
}
