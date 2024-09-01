using DentalApplication.Common.Interfaces.IServices;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DentalInfrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly string twilioAccountSid = "ACad4f921ada3bfce728bc69e68b8565dc";
        private readonly string twilioAuthToken = "455731b732af89e8303dc5daa7983b09";

        public Task SendMessage()
        {
            //var client = Client(twilioAccountSid, twilioAuthToken);

            //// Send SMS using a Messaging Service
            //MessageResource.Create(
            //    to: new PhoneNumber("+355692128147"),
            //    messagingServiceSid: twilioMessagingServiceSid,
            //    body: "Hello from your Alpha sender 👏"
            //);

            TwilioClient.Init(twilioAccountSid, twilioAuthToken);

            var message = MessageResource.Create(
                new PhoneNumber("+355692128147"),
                from: new PhoneNumber("+10987654321"),
                body: "Hello World!"
            );
            Console.WriteLine(message.Sid);

            return Task.CompletedTask;



        }
    }
}
