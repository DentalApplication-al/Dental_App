namespace DentalDomain.Users.Clients
{
    public class Client : User
    {
        public static Client Create(
            DateTime birthday,
            string first_name,
            string last_name,
            string email,
            string phone)
        {
            return new Client()
            {
                Birthday = birthday,
                FirstName = first_name,
                LastName = last_name,
                Email = email,
                Phone = phone,
                CreatedOn = DateTime.Now.ToUniversalTime(),
            };
        }
    }
}
