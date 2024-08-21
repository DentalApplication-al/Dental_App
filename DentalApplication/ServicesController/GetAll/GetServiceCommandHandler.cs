using DentalApplication.Common.Interfaces.IRepositories;
using MediatR;

namespace DentalApplication.ServicesController.Get
{
    public class GetServiceCommandHandler : IRequestHandler<GetAllServicesCommand, List<ServiceResponse>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceCommandHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<List<ServiceResponse>> Handle(GetAllServicesCommand request, CancellationToken cancellationToken)
        {
            var result = await _serviceRepository.GetClinicServices(request.clinic_id.Value);
            return ServiceResponse.Map(result);
        }
    }
}
