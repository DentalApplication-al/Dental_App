using System.ComponentModel.DataAnnotations;

namespace DentalDomain.Clinics
{
    public class Clinic
    {
        [Key]
        public Guid Id { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string Nipt { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public static Clinic Create(
            string name,
            string address,
            string nipt,
            string phone,
            string email)
        {
            return new Clinic
            {
                Name = name,
                Address = address,
                Nipt = nipt,
                Phone = phone,
                Email = email,
                CreatedOn = DateTime.UtcNow.ToUniversalTime(),

            };
        }
        private Clinic()
        {

        }
    }
}
