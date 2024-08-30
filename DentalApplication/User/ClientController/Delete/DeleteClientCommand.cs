using DentalApplication.Common;
using DentalApplication.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.ClientController.Delete
{
    public class DeleteClientCommand : CommandBase, IRequest
    {
        [SwaggerIgnore]
        [FromRoute(Name = "id")]
        public Guid? id { get; set; }
    }
}
