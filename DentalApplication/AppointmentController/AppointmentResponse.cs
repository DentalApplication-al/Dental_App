using DentalApplication.ServicesController.DTO;
using DentalApplication.User.ClientController.DTO;
using DentalApplication.User.StaffController.DTO;
using DentalDomain.Users.Staffs;

namespace DentalApplication.AppointmentController
{
    public class AppointmentResponse
    {
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public decimal price { get; set; }
        public StaffResponse doctor { get; set; }
        public ClientResponse client { get; set; }
        public ServiceResponse service { get; set; }
    }
}
