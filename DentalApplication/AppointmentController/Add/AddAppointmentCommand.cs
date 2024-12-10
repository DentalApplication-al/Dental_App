using DentalApplication.AppointmentController.DTO;
using MediatR;

namespace DentalApplication.AppointmentController.Add
{
    public class AddAppointmentCommand : AddAppointmentRequest, IRequest<Guid>
    {
        public Guid? clinic_id { get; set; }

        public Guid? logged_in_staff_id { get; set; }
        //public AddAppointmentCommand(AddAppointmentRequest request)
        //{
        //    // Mapping properties from AddAppointmentRequest
        //    this.existingClient = request.existingClient;
        //    this.newClient = request.newClient;
        //    this.serviceId = request.serviceId;
        //    this.doctors = request.doctors;
        //    this.price = request.price;
        //    this.description = request.description;
        //    this.date = request.date;
        //    this.startTime = request.startTime;
        //    this.endTime = request.endTime;
        //    this.isApproved = request.isApproved;

        //    // Initialize additional properties to null or default values if necessary
        //    this.clinic_id = null;
        //    this.loged_in_staff_id = null;
        //}
    }
    
}
