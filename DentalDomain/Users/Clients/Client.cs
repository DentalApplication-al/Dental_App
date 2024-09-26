using DentalDomain.Appointments;
using DentalDomain.Files;
using DentalDomain.Users.Enums;

namespace DentalDomain.Users.Clients
{
    public class Client : User
    {
        public string? Description { get; set; }
        public bool? HeartCondition { get; set; }
        public bool? Diabetes { get; set; }
        public bool? Hypertension { get; set; }
        public bool? BleedingDisorders { get; set; }
        public bool? Immunocompromised { get; set; }
        public string? OtherConditions { get; set; }
        public string? Allergies { get; set; }
        public string? CurrentMedications { get; set; }
        public string? SpecialNotes { get; set; }
        public Gender Gender { get; set; }
        public List<Appointment> Appointments { get; set; } = new();
        public List<Documents> CLientFiles { get; set; } = new();

        public static Client Create(
            DateOnly birthday,
            string first_name,
            string last_name,
            string email,
            string phone,
            Guid clinic_id,
            string? description,
            Gender gender,
            bool? heart_condition,
            bool? diabetes,
            bool? hypertension,
            bool? bleeding_disorders,
            bool? immunocompromised,
            string? allergies,
            string? other_conditions,
            string? current_medications,
            string? special_notes)
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
                Gender = gender,
                HeartCondition = heart_condition,
                Diabetes = diabetes,
                Hypertension = hypertension,
                BleedingDisorders = bleeding_disorders,
                Immunocompromised = immunocompromised,
                Allergies = allergies,
                OtherConditions = other_conditions,
                CurrentMedications = current_medications,
                SpecialNotes = special_notes
            };
        }
        public static Client Create() => new();

        public void Update(
            string new_first_name, 
            string new_last_name, 
            string new_phone, 
            string new_email, 
            DateOnly new_birthday,
            string? description,
            Gender gender,
            bool? heart_condition,
            bool? diabetes,
            bool? hypertension,
            bool? bleeding_disorders,
            bool? immunocompromised,
            string? allergies,
            string? other_conditions,
            string? current_medications,
            string? special_notes)
        {
            FirstName = new_first_name;
            LastName = new_last_name;
            Email = new_email;
            Phone = new_phone;
            Birthday = new_birthday;
            Description = description;

            Gender =gender;
            HeartCondition = heart_condition;
            Diabetes= diabetes;
            Hypertension = hypertension;
            BleedingDisorders = bleeding_disorders;
            Immunocompromised= immunocompromised;
            Allergies= allergies;
            OtherConditions = other_conditions;
            CurrentMedications = current_medications;
            SpecialNotes = special_notes;
        }
    }
}
