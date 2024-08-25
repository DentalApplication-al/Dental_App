using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.ServicesController.DTO;
using MediatR;

namespace DentalApplication.ServicesController.Get
{
    public class GetServiceCommandHandler : IRequestHandler<GetAllServicesCommand, PaginatedResponse<ListService>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceCommandHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<PaginatedResponse<ListService>> Handle(GetAllServicesCommand request, CancellationToken cancellationToken)
        {
            var result = await _serviceRepository
                .GetPaginatedClinicServices(request.clinic_id.Value, request.take, request.page, request.search);
            return result;
        }
    }
}
