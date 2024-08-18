using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using MediatR;

namespace DentalApplication.User.StaffController.GetById
{
    public class GetStaffByIdCommandHandler : IRequestHandler<GetStaffByIdCommand, StaffResponse>
    {
        private readonly IStaffRepository _staffRepository;

        public GetStaffByIdCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<StaffResponse> Handle(GetStaffByIdCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetstaffByIdAsync(request.staff_id, request.clinic_id) ??
                throw new NotFoundException("Staff could not be found");

            return StaffResponse.Map(staff);
        }
    }
}
