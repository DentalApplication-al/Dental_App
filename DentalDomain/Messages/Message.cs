namespace DentalDomain.Messages
{
    public class Message : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string? ReceiverName { get; set; }
        public Guid ClinicId { get; set; }
        public bool IsSent { get; set; }

    }
}
