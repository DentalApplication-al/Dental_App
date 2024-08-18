using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.ServicesController.Delete
{
    public class DeleteServiceCOmmandHandler : IRequestHandler<DeleteServiceCommand, bool>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public DeleteServiceCOmmandHandler(IServiceRepository serviceRepository, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _serviceRepository = serviceRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            return await _serviceRepository.DeleteAsync(request.service_id.Value);
        }
    }
}
