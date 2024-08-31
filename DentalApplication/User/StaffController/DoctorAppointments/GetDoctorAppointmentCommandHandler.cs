using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using MediatR;

namespace DentalApplication.User.StaffController.DoctorAppointments
{
    public class GetDoctorAppointmentCommandHandler : IRequestHandler<GetDoctorAppointmentCommand, PaginatedResponse<ListAppointment>>
    {
        private readonly IStaffRepository _staffRepository;

        public GetDoctorAppointmentCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<PaginatedResponse<ListAppointment>> Handle(GetDoctorAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _staffRepository
                .GetDoctorAppointments(
                request.id,
                request.clinic_id.Value,
                request.page,
                request.take,
                request.search,
                request.isHistory);

            return result;
        }
    }
}
