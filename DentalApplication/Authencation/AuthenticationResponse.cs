using DentalApplication.User.StaffController.DTO;

namespace DentalContracts.AuthenticationContracts
{
    public class AuthenticationResponse
    {
        public string token { get; set; }
        public StaffResponse staff { get; set; }
    }
}
