using DentalDomain.Appointments;

namespace DentalDomain.Users.Clients
{
    public class Client : User
    {
        //public List<Appointment> Appointments { get; set; } = [];

        public static Client Create(
            DateTime birthday,
            string first_name,
            string last_name,
            string email,
            string phone,
            Guid clinic_id)
        {
            return new Client()
            {
                Birthday = birthday,
                FirstName = first_name,
                LastName = last_name,
                Email = email,
                Phone = phone,
                CreatedOn = DateTime.Now.ToUniversalTime(),
                ClinicId = clinic_id,
            };
        }
        public static Client Create() => new();

        public void Update(
            string new_first_name, 
            string new_last_name, 
            string new_phone, 
            string new_email, 
            DateTime new_birthday)
        {
            FirstName = new_first_name;
            LastName = new_last_name;
            Email = new_email;
            Phone = new_phone;
            Birthday = new_birthday;
        }
    }
}
