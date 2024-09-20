using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using MediatR;

namespace DentalApplication.User.ClientController.ClientAppointments
{
    public class GetClientAppointmentsCommandHandler : IRequestHandler<GetClientAppointmentsCommand, PaginatedResponse<ListAppointment>>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientAppointmentsCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<PaginatedResponse<ListAppointment>> Handle(GetClientAppointmentsCommand request, CancellationToken cancellationToken)
        {
            return await _clientRepository.GetClientAppointments(
                request.id,
                request.clinic_id.Value,
                request.page,
                request.take,
                request.search,
                request.isHistory);
        }
    }
}
