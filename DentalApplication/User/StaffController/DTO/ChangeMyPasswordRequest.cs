namespace DentalApplication.User.StaffController.DTO
{
    public class ChangeMyPasswordRequest
    {
        public string? oldPassword { get; set; }
        public string? newPassword { get; set; }
    }
}
