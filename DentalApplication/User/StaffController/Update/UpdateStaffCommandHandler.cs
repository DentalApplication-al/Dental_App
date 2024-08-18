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

        public UpdateStaffCommandHandler(IStaffRepository staffRepository, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _staffRepository = staffRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<StaffResponse> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetByIdAsync(request.staff_id.Value) ??
                throw new NotFoundException(_stringLocalizer.Get(Error.NOT_FOUND, _stringLocalizer["Staff"]));

            staff.Update(
                request.new_first_name,
                request.new_last_name,
                request.new_email,
                request.new_phone,
                request.new_birthday.Value,
                request.new_username,
                request.new_password,
                request.new_role.Value
                );
            await _staffRepository.UpdateAsync(staff);

            await _staffRepository.SaveChangesAsync();

            return StaffResponse.Map(staff);
        }
    }
}
