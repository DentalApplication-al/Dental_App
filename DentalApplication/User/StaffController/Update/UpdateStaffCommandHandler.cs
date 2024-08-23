using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DentalApplication.User.StaffController.Update
{
    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, StaffResponse>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IBlobStorage _blob;

        public UpdateStaffCommandHandler(IStaffRepository staffRepository, IStringLocalizer<SharedResource> stringLocalizer, IBlobStorage blob)
        {
            _staffRepository = staffRepository;
            _stringLocalizer = stringLocalizer;
            _blob = blob;
        }

        public async Task<StaffResponse> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetByIdAsync(request.staff_id.Value) ??
                throw new NotFoundException(_stringLocalizer.Get(Error.NOT_FOUND, _stringLocalizer["Staff"]));
            var profile = "";

            staff.Update(
                request.new_first_name,
                request.new_last_name,
                request.new_email,
                request.new_phone,
                request.new_birthday.Value,
                request.new_role.Value,
                request.new_job_type,
                profile,
                request.new_start_time,
                request.new_end_time
                );
            await _staffRepository.UpdateAsync(staff);

            await _staffRepository.SaveChangesAsync();

            return StaffResponse.Map(staff);
        }
    }
}
