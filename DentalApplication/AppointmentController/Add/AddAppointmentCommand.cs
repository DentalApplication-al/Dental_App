using DentalApplication.AppointmentController.DTO;
using MediatR;

namespace DentalApplication.AppointmentController.Add
{
    public class AddAppointmentCommand : AddAppointmentRequest, IRequest<Guid>
    {
        public Guid? clinic_id { get; set; }

        public Guid? logged_in_staff_id { get; set; }


        public void MapClinic()
        {
            newClient.clinic_id = clinic_id.Value;
            newClient.loged_in_staff_id = clinic_id.Value;
        }
        public bool IsExistingClient()
        {
            if (existingClient.HasValue)
            {
                return true;
            }
            return false;
        }
    }
    
}
