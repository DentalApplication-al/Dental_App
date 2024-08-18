using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Common.Interfaces.IServices;
using DentalApplication.Errors;
using DentalApplication.Resources;
using DentalDomain.Users.Staffs;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.User.StaffController.Add
{
    public class AddStaffCommandHandler : IRequestHandler<AddStaffCommand, StaffResponse>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IEmailService _emailService;
        private readonly IServiceRepository _serviceRepository;
        public AddStaffCommandHandler(IStaffRepository staffRepository, IStringLocalizer<SharedResource> stringLocalizer, IEmailService emailService, IServiceRepository serviceRepository)
        {
            _staffRepository = staffRepository;
            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
            _serviceRepository = serviceRepository;
        }

        public async Task<StaffResponse> Handle(AddStaffCommand request, CancellationToken cancellationToken)
        {
            if (await _staffRepository.Exists(request.email))
            {
                throw new BadRequestException(_stringLocalizer.Get(Error.USED_USERNAME));
            }
            var staff = Staff.Create(
                request.first_name,
                request.last_name,
                request.email,
                request.phone,
                request.birthday.Value,
                request.role.Value,
                request.clinic_id
                );

            if (request.services.Count > 0)
            {
                var services = await _serviceRepository.GetServiceByIds(request.services);
                staff.StaffServices = services;
            }

            var email = await _emailService.SendPasswordAsync(staff.Email, staff.Password);

            if (email.WasSent)
            {
                await _staffRepository.AddAsync(staff);
                await _staffRepository.SaveChangesAsync();
                return StaffResponse.Map(staff);
            }
            throw new BadRequestException("The email adress does not exist");
        }
    }
}