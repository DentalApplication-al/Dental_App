using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.Resources;
using DentalApplication.User.StaffController.DTO;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.User.StaffController.Update
{
    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, StaffResponse>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IBlobStorage _blob;
        private readonly IServiceRepository _serviceRepository;

        public UpdateStaffCommandHandler(IStaffRepository staffRepository, IStringLocalizer<SharedResource> stringLocalizer, IBlobStorage blob, IServiceRepository serviceRepository)
        {
            _staffRepository = staffRepository;
            _stringLocalizer = stringLocalizer;
            _blob = blob;
            _serviceRepository = serviceRepository;
        }

        public async Task<StaffResponse> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetById(request.id.Value, request.clinic_id.Value) ??
                throw new NotFoundException(_stringLocalizer.Get(Error.NOT_FOUND, _stringLocalizer["Staff"]));
            
            var profile = staff.ProfilePic;

            var services = await _serviceRepository.GetServiceByIds(request.clinic_id.Value, request.services);
            
            if (request.picture !=null)
            {
                var upload = await _blob.Upload(request.picture);
                if (upload.hasSucceded)
                {
                    profile = upload.data;
                    if (!string.IsNullOrEmpty(staff.ProfilePic))
                    {
                        await _blob.DeleteBlobAsync(staff.ProfilePic);
                    }
                }
            }
            staff.Update(
                request.first_name,
                request.last_name,
                request.email,
                request.phone,
                request.birthday.Value,
                request.role.Value,
                request.job_type,
                profile,
                request.start_time,
                request.end_time
                );

            staff.StaffServices = services;

            await _staffRepository.UpdateAsync(staff);

            await _staffRepository.SaveChangesAsync();

            return StaffResponse.Map(staff);
        }
    }
}
