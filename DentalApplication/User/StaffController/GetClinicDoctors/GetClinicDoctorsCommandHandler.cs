using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.StaffController.DTO;
using MediatR;

namespace DentalApplication.User.StaffController.GetClinicDoctors
{
    public class GetClinicDoctorsCommandHandler : IRequestHandler<GetClinicDoctorsCommand, List<ListStaff>>
    {
        private readonly IStaffRepository _staffRepository;

        public GetClinicDoctorsCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<List<ListStaff>> Handle(GetClinicDoctorsCommand request, CancellationToken cancellationToken)
        {
            var doctors = await _staffRepository.GetClinicDoctors(request.logged_in_staff_id.Value, request.clinic_id.Value);
            return doctors;
        }
    }
}
