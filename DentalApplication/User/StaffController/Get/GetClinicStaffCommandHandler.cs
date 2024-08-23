using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using MediatR;

namespace DentalApplication.User.StaffController.Get
{
    public class GetClinicStaffCommandHandler : IRequestHandler<GetClinicStaffCommand, PaginatedResponse<ListStaff>>
    {
        private readonly IStaffRepository _staffRepository;

        public GetClinicStaffCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<PaginatedResponse<ListStaff>> Handle(GetClinicStaffCommand request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetPaginatedClinicStaff(request.loged_in_staff_id.Value, request.clinic_id.Value, request.page, request.take);

            return staff;
        }
    }
}
