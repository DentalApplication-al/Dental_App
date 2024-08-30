using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.ServicesController.DTO;
using MediatR;

namespace DentalApplication.ServicesController.GetAllForDoctors
{
    public class GetServicesForDoctorsCommandHandler : IRequestHandler<GetAllForDoctorsCommand, List<ListService>>
    {
        private readonly IServiceRepository _repository;

        public GetServicesForDoctorsCommandHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ListService>> Handle(GetAllForDoctorsCommand request, CancellationToken cancellationToken)
        {
            var services = await _repository.GetServicesForDoctors(request.clinic_id.Value);

            return services;
        }
    }
}
