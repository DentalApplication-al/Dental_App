using DentalDomain.Appointments;
using DentalDomain.Services;
using DentalDomain.Users.Enums;
using System.Text;

namespace DentalDomain.Users.Staffs
{
    public class Staff : User
    {
        public string Password { get; private set; }
        public Role Role { get; private set; }
        public string? OTP { get; private set; }
        public string? JobType { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? ProfilePic { get; set; }
        public StaffStatus Status { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Service> StaffServices { get; set; } = new();
        public Gender Gender { get; set; }
        public static Staff Create(
            string first_name,
            string last_name,
            string email,
            string phone,
            DateOnly? birthday,
            Role role,
            Guid clinicId,
            string? start_time,
            string? end_time,
            string? profile,
            string? job_type,
            Gender gender
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
                Password = GeneratePassword(),
                Role = role,
                ClinicId = clinicId,
                StartTime = start_time,
                EndTime = end_time,
                ProfilePic = profile,
                CreatedOn = DateTime.Now.ToUniversalTime(),
                JobType = job_type
            };
        }

        public static string CreateAdminPassword()
        {
            return "Password1.";
        }

        public void Update(
            string first_name,
            string last_name,
            string email,
            string phone,
            DateOnly birthday,
            Role role,
            string? job_type,
            string? picture,
            string? start_time,
            string? end_time,
            StaffStatus status,
            Gender gender
            )

        {
            FirstName = first_name;
            LastName = last_name;
            Email = email;
            Phone = phone;
            Birthday = birthday;
            Role = role;
            JobType = job_type;
            StartTime = start_time;
            EndTime = end_time;
            ProfilePic = picture ?? ProfilePic;
            Status = status;
            Gender = gender;
        }

        public void SetOTP()
        {
            OTP = RandomOTPGenerator();
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
        public static string RandomOTPGenerator()
        {
            Random random = new Random();
            int password = random.Next(100_000, 999_999);
            return password.ToString();
        }

        public static string GeneratePassword()
        {
            var length = 8;
            Random Random = new();

            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            // Ensure at least one of each required character type
            var password = new StringBuilder();
            password.Append(upperCase[Random.Next(upperCase.Length)]);
            password.Append(numbers[Random.Next(numbers.Length)]);
            password.Append(symbols[Random.Next(symbols.Length)]);

            // Fill the rest with lowercase letters
            var remainingLength = length - password.Length;
            var allLowerCase = lowerCase.ToCharArray();
            for (var i = 0; i < remainingLength; i++)
            {
                password.Append(allLowerCase[Random.Next(allLowerCase.Length)]);
            }

            // Shuffle the characters to ensure randomness
            return new string(password.ToString().OrderBy(_ => Random.Next()).ToArray());
        }


        private Staff() { }
    }
}
