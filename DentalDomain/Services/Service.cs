using DentalDomain.Users.Staffs;

namespace DentalDomain.Services
{
    public class Service : BaseEntity
    {
        public decimal Price { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public int Duration { get; private set; }
        public List<Staff> ServiceStaff { get; set; } = new();


        public static Service Create(
            decimal price,
            string name,
            string? description,
            int duration,
            Guid clinic_id)
        {
            return new Service()
            {
                Id = Guid.NewGuid(),
                Price = price,
                Name = name,
                Description = description,
                Duration = duration,
                ClinicId = clinic_id,
                CreatedOn = DateTime.UtcNow,
            };
        }

        public void Update(
            decimal price,
            string name,
            string? description,
            int duration
            )
        {
            Price = price;
            Name = name;
            Description = description;
            Duration = duration;
        }



        public static Service Create()
        {
            return new Service();
        }
        private Service() { }

    }
}
