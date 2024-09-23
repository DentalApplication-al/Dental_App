using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Common.Interfaces.IServices;
using DentalApplication.Errors;
using DentalDomain.Users.Enums;
using MediatR;

namespace DentalApplication.User.StaffController.ChangeStatus
{
    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUserTokenService _userTokenService;
        public ChangeStatusCommandHandler(IStaffRepository staffRepository, IUserTokenService userTokenService)
        {
            _staffRepository = staffRepository;
            _userTokenService = userTokenService;
        }

        public async Task Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository
                .GetById(request.staff_id, request.clinic_id.Value) ??
                throw new NotFoundException("The staff could not be found.");

            staff.Status = request.status;

            await _staffRepository.UpdateAsync(staff);
            await _staffRepository.SaveChangesAsync();

            if (request.status == StaffStatus.PASSIVE)
            {
                await _userTokenService.MakeTokenInvalidAsync(staff.Id);
            }
        }
    }
}
