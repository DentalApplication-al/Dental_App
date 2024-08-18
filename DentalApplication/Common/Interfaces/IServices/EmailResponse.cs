namespace DentalApplication.Common.Interfaces.IServices
{
    public class EmailResponse
    {
        public bool WasSent { get; set; }
        public bool Result { get; set; }

        public static EmailResponse Success()
        {
            return new EmailResponse
            {
                WasSent = true,
                Result = true
            };
        }

        public static EmailResponse Fail()
        {
            return new EmailResponse
            {
                WasSent = false,
                Result = false
            };
        }
    }
}
