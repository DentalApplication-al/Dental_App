using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using MediatR;

namespace DentalApplication.AppointmentController.GetAll
{
    public class GetAllAppointmentsCommandHandler : IRequestHandler<GetAllAppointmentsCommand, PaginatedResponse<ListAppointment>>
    {
        private readonly IAppointmentRepository _appointmentsRepository;

        public GetAllAppointmentsCommandHandler(
            IAppointmentRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        public async Task<PaginatedResponse<ListAppointment>> Handle(GetAllAppointmentsCommand request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentsRepository.GetPaginatedAppointments(request.clinic_id.Value, request.take, request.page, request.search);
            return appointments;
        }
    }
}
