namespace DentalDomain.Users
{
    public class User : BaseEntity
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public DateTime Birthday { get; protected set; }
        public string? Phone { get; protected set; }
    }
}
