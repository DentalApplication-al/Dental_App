using DentalDomain.Appointments;

namespace DentalDomain.Users.Clients
{
    public class Client : User
    {
        public string? Description { get; set; }
        public List<Appointment> Appointments { get; set; } = new();

        public static Client Create(
            DateOnly birthday,
            string first_name,
            string last_name,
            string email,
            string phone,
            Guid clinic_id,
            string? description)
        {
            return new Client()
            {
                Birthday = birthday,
                FirstName = first_name,
                LastName = last_name,
                Email = email,
                Phone = phone,
                CreatedOn = DateTime.Now,
                ClinicId = clinic_id,
                Description = description,
            };
        }
        public static Client Create() => new();

        public void Update(
            string new_first_name, 
            string new_last_name, 
            string new_phone, 
            string new_email, 
            DateOnly new_birthday,
            string? description)
        {
            FirstName = new_first_name;
            LastName = new_last_name;
            Email = new_email;
            Phone = new_phone;
            Birthday = new_birthday;
            Description = description;
        }
    }
}
