using DentalApplication.Common;
using DentalApplication.Swagger;
using DentalApplication.User.ClientController.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.ClientController.Update
{
    public class UpdateClientCommand : CommandBase, IRequest<Guid>
    {
        [SwaggerIgnore]
        public Guid? id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public DateOnly? birthday { get; set; }
        public string? description { get; set; }
    }
}
