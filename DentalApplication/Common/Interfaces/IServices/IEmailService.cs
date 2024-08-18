namespace DentalApplication.Common.Interfaces.IServices
{
    public interface IEmailService
    {
        Task<EmailResponse> SendOTPEmailAsync(string email, string otp);
        Task<EmailResponse> SendPasswordAsync(string email, string password);
    }
}
