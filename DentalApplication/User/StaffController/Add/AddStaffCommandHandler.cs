using DentalApplication.Common.Interfaces.IBlobStorages;
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
        private readonly IBlobStorage _blobStorage;
        public AddStaffCommandHandler(IStaffRepository staffRepository, IStringLocalizer<SharedResource> stringLocalizer, IEmailService emailService, IServiceRepository serviceRepository, IBlobStorage blobStorage)
        {
            _staffRepository = staffRepository;
            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
            _serviceRepository = serviceRepository;
            _blobStorage = blobStorage;
        }

        public async Task<StaffResponse> Handle(AddStaffCommand request, CancellationToken cancellationToken)
        {
            if (await _staffRepository.Exists(request.email))
            {
                throw new BadRequestException(_stringLocalizer.Get(Error.USED_USERNAME));
            }
            string profilePicture = "";
            if (request.picture != null)
            {
                var uploadRequest = await _blobStorage.Upload(request.picture);
                if (uploadRequest.hasSucceded)
                {
                    profilePicture = uploadRequest.data;
                }
            }
            var staff = Staff.Create(
                request.first_name,
                request.last_name,
                request.email,
                request.phone,
                request.birthday,
                request.role.Value,
                request.clinic_id.Value,
                request.start_time,
                request.end_time,
                profilePicture,
                request.job_type
                );

            if (request.services != null && request.services.Count > 0)
            {
                var services = await _serviceRepository.GetServiceByIds(request.services);
                staff.StaffServices = services;
            }

            var email = await _emailService.SendPasswordAsync(staff.Email, staff.Password);

            if (email.WasSent)
            {
                await _staffRepository.AddAsync(staff);
                await _staffRepository.SaveChangesAsync();
                var result = StaffResponse.Map(staff);
                result.picture = _blobStorage.GetLink(profilePicture);
                return result;
            }
            await _blobStorage.DeleteBlobAsync(profilePicture);

            throw new BadRequestException("The email adress does not exist");
        }
    }
}