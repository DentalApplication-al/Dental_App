using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalApplication.User.StaffController.DoctorAppointments
{
    public class GetDoctorAppointmentCommand : CommandBase, IRequest<PaginatedResponse<ListAppointment>>
    {
        [FromRoute(Name = "id")]
        public Guid id { get; set; }
        public int page { get; set; }
        public int take { get; set; }
        public string? search { get; set; }
    }
}
