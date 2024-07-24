using System.ComponentModel.DataAnnotations;

namespace DentalDomain.Users.SuperAdmin
{
    public class SuperAdmin
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
