using DentalApplication.User.StaffController;

namespace DentalContracts.AuthenticationContracts
{
    public class AuthenticationResponse
    {
        public string token { get; set; }
        public StaffResponse staff { get; set; }
    }
}
