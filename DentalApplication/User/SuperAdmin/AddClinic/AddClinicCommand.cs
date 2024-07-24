using MediatR;

namespace DentalApplication.User.SuperAdmin.AddClinic
{
    public class AddClinicCommand : IRequest<ClinicResponse>
    {
        public string clinic_name { get; set; }
        public string clinic_address { get; set; }
        public string clinic_nipt { get; set; }
        public string admin_first_name { get; set; }
        public string admin_last_name { get; set; }
        public string admin_username { get; set; }
        public string admin_phone { get; set; }
        public string admin_email { get; set; }
        public DateTime admin_birthday { get; set; }
    }
}
