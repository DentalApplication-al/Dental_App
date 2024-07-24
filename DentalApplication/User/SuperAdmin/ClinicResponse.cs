
using DentalDomain;
using DentalDomain.Clinics;

namespace DentalApplication.User.SuperAdmin
{
    public class ClinicResponse
    {
        public string name { get; private set; }
        public string address { get; private set; }
        public string nipt { get; private set; }
        public string phone { get; private set; }
        private ClinicResponse(Clinic clinic)
        {
            name = clinic.Phone;
            address = clinic.Address;
            nipt = clinic.Nipt;
            phone = clinic.Phone;
        }
        public static ClinicResponse Map(Clinic clinic) 
        {
            return new ClinicResponse(clinic);
        }
        public static List<ClinicResponse> Map (List<Clinic> clinics)
        {
            return clinics.Select(a => new ClinicResponse(a)).ToList();
        }
    }
}
