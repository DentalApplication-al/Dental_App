using DentalApplication.Common;
using DentalApplication.Swagger;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DentalApplication.User.ClientController.AddClientFile
{
    public class AddClientFileCommand : CommandBase, IRequest
    {
        [SwaggerIgnore]
        public Guid clientId { get; set; }
        [SwaggerIgnore]
        public List<IFormFile> files { get; set; }
    }
}
