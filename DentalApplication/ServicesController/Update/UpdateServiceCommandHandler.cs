﻿using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.ServicesController.Update
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ServiceResponse>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IStaffRepository _staffRepository;
        public UpdateServiceCommandHandler(IServiceRepository serviceRepository, IStringLocalizer<SharedResource> stringLocalizer, IStaffRepository staffRepository)
        {
            _serviceRepository = serviceRepository;
            _stringLocalizer = stringLocalizer;
            _staffRepository = staffRepository;
        }

        public async Task<ServiceResponse> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(request.service_id.Value) ??
                throw new NotFoundException(_stringLocalizer.Get(Error.NOT_FOUND, _stringLocalizer["Service"]));
            var serviceDoctors = await _staffRepository.GetStaffByIdsAsync(request.doctors);
            service.Update(
                request.new_price.Value,
                request.new_name,
                request.new_description,
                request.new_duration.Value
            );
            service.ServiceStaff = serviceDoctors;

            await _serviceRepository.UpdateAsync(service);
            await _serviceRepository.SaveChangesAsync();
            return ServiceResponse.Map(service);
        }
    }
}