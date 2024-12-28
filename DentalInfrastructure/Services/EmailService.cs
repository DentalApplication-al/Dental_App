using DentalApplication.Common.Interfaces.IServices;
using Serilog;
using System.Net;
using System.Net.Mail;

namespace DentalInfrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpClient;
        private readonly int _port;
        private readonly string _fromEmail;
        private readonly string _password;

        public EmailService()
        {
            _smtpClient = "smtp.gmail.com";
            _port = 587;
            _fromEmail = "fastsolutionsuport@gmail.com"; // Replace with your Gmail address
            _password = "jgle gtot xhuc esue"; // Replace with your app password
        }
        public async Task<EmailResponse> SendOTPEmailAsync(string email, string otp)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail),
                Subject = "One Time Password",
                Body = GetOtpTemplate(otp),
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            return await SendEmail(mailMessage);
        }
        public async Task<EmailResponse> SendPasswordAsync(string email, string password)
        {
            var message = new MailMessage()
            {
                From = new MailAddress(_fromEmail),
                Subject = "Login Credintials",
                Body = GetPasswordTemplate(email, password),
                IsBodyHtml = true,
            };
            message.To.Add(email);

            return await SendEmail(message);
        }







        private async Task<EmailResponse> SendEmail(MailMessage mailMessage)
        {
            var smtpClient = new SmtpClient(_smtpClient)
            {
                Port = _port,
                Credentials = new NetworkCredential(_fromEmail, _password),
                EnableSsl = true,
            };
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return EmailResponse.Success();
            }
            catch (Exception ex)
            {
                Log.Error("Failed to send email because: " + ex.Message);
                return EmailResponse.Fail();
            }
        }
        private string GetOtpTemplate(string otp)
        {
            var template = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Your OTP Code</title>
</head>
<body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;"">
    <div style=""max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);"">
        <div style=""text-align: center; padding-bottom: 20px;"">
            <h1 style=""margin: 0; color: #333333;"">Your OTP Code</h1>
        </div>
        <div style=""text-align: center; padding: 20px 0;"">
            <p style=""font-size: 18px; color: #666666;"">Use the following OTP code to complete your verification process:</p>
            <div style=""font-size: 24px; font-weight: bold; color: #333333; padding: 10px 20px; background-color: #f8f8f8; border-radius: 5px; display: inline-block; margin-top: 20px;"">{otp}</div>
        </div>
        <div style=""text-align: center; padding-top: 20px; font-size: 12px; color: #999999;"">
            <p>If you did not request this code, please ignore this email.</p>
        </div>
    </div>
</body>
</html>
";
            return template;
        }

        private string GetPasswordTemplate(string email, string password)
        {
            var template = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Account Credentials</title>
</head>
<body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;"">
    <div style=""max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);"">
        <div style=""text-align: center; padding-bottom: 20px;"">
            <h1 style=""margin: 0; color: #333333;"">Your Account Credentials</h1>
        </div>
        <div style=""padding: 20px 0;"">
            <p style=""font-size: 18px; color: #666666; text-align: center;"">Hello,</p>
            <p style=""font-size: 16px; color: #666666; text-align: center;"">Your account has been created. You can log in to the platform using the following credentials:</p>
            <div style=""margin-top: 20px; text-align: center;"">
                <p style=""font-size: 16px; color: #333333; margin: 10px 0;""><strong>Email:</strong> {email}</p>
                <p style=""font-size: 16px; color: #333333; margin: 10px 0;""><strong>Password:</strong> {password}</p>
            </div>
        </div>
        <div style=""text-align: center; padding-top: 20px; font-size: 12px; color: #999999;"">
            <p>If you did not request this, please contact support immediately.</p>
        </div>
    </div>
</body>
</html>
";
            return template;
        }

    }
}
