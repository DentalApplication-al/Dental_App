using DentalDomain.Users.Enums;

namespace DentalDomain.Users.Staffs
{
    public class Staff : User
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }
        public static Staff Create(
            string first_name,
            string last_name,
            string email,
            string phone,
            DateTime birthday,
            string username,
            string password,
            Role role,
            Guid clinicId
            )
        {
            return new Staff()
            {
                Id = Guid.NewGuid(),
                FirstName = first_name, 
                LastName = last_name,
                Email = email,
                Phone = phone,
                Birthday = birthday,
                Username = username,
                Password = password,
                Role = role,
                ClinicId = clinicId,
                CreatedOn = DateTime.Now.ToUniversalTime(),
            };
        }
        
        public static string CreateAdminPassword()
        {
            return "Password1.";
        }
        
        private Staff() { }
    }
}
