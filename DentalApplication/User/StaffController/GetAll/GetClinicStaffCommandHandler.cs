using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.StaffController.DTO;
using MediatR;

namespace DentalApplication.User.StaffController.GetAll
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
            var staff = await _staffRepository
                .GetPaginatedClinicStaff(
                request.loged_in_staff_id.Value, 
                request.clinic_id.Value, 
                request.page, 
                request.take,
                request.search);

            return staff;
        }
    }
}
