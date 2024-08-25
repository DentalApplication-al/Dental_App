using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.ServicesController.DTO;
using DentalDomain.Services;
using MediatR;

namespace DentalApplication.ServicesController.Add
{
    public class AddServiceCommandHandler : IRequestHandler<AddServiceCommand, ServiceResponse>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IStaffRepository _staffRepository;

        public AddServiceCommandHandler(IServiceRepository serviceRepository, IStaffRepository staffRepository)
        {
            _serviceRepository = serviceRepository;
            _staffRepository = staffRepository;
        }

        public async Task<ServiceResponse> Handle(AddServiceCommand request, CancellationToken cancellationToken)
        {
            var service = Service.Create(
                request.price.Value,
                request.name,
                request.description,
                request.duration.Value,
                request.clinic_id.Value);

            var doctors = await _staffRepository.GetStaffByIdsAsync(request.doctors);

            service.ServiceStaff = doctors;

            await _serviceRepository.AddAsync(service);
            await _serviceRepository.SaveChangesAsync();

            return ServiceResponse.Map(service);
        }
    }
}
