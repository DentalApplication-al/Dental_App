using DentalApplication.Common.Interfaces.IRepositories;
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

        public AddStaffCommandHandler(IStaffRepository staffRepository, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _staffRepository = staffRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<StaffResponse> Handle(AddStaffCommand request, CancellationToken cancellationToken)
        {
            if (await _staffRepository.Exists(request.username, request.email))
            {
                throw new BadRequestException(_stringLocalizer.Get(Error.USED_USERNAME)); 
            }
            var password = "Password1.";
            var staff = Staff.Create(
                request.first_name,
                request.last_name,
                request.email,
                request.phone,
                request.birthday.Value,
                request.username,
                password,
                request.role.Value,
                request.clinic_id
                );
            await _staffRepository.AddAsync(staff);
            await _staffRepository.SaveChangesAsync();
            return new StaffResponse(staff);
        }
    }
}
