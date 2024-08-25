using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.Resources;
using DentalApplication.ServicesController.DTO;
using DentalApplication.User.StaffController.DTO;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.ServicesController.GetById
{
    public class GetServiceByIdCommandHandler : IRequestHandler<GetServiceByIdCommand, ServiceById>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public GetServiceByIdCommandHandler(IServiceRepository serviceRepository, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _serviceRepository = serviceRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<ServiceById> Handle(GetServiceByIdCommand request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetServiceById(request.clinic_id.Value, request.service_id.Value) ??
                throw new NotFoundException(_stringLocalizer.Get(Error.NOT_FOUND, _stringLocalizer["Service"]));
            
            return service;
        }
    }
}
