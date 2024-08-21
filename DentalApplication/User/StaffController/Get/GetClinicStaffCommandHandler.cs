using DentalApplication.Common.Interfaces.IRepositories;
using MediatR;

namespace DentalApplication.User.StaffController.Get
{
    public class GetClinicStaffCommandHandler : IRequestHandler<GetClinicStaffCommand, List<StaffResponse>>
    {
        private readonly IStaffRepository _staffRepository;

        public GetClinicStaffCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<List<StaffResponse>> Handle(GetClinicStaffCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetClinicStaff(request.loged_in_staff_id.Value, request.clinic_id.Value);

            return StaffResponse.Map(staff);
        }
    }
}
