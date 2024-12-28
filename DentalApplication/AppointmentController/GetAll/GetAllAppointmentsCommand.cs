using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using MediatR;

namespace DentalApplication.AppointmentController.GetAll
{
    public class GetAllAppointmentsCommand : PaginationProps, IRequest<PaginatedResponse<ListAppointment>>
    {
        public GetAllAppointmentsCommand(Props? props) : base(props)
        {
        }
    }
}
