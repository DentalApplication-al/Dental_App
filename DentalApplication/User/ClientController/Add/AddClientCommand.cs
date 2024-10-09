using DentalApplication.Common;
using DentalDomain.Users.Enums;
using MediatR;

namespace DentalContracts.UserContracts.ClientContracts
{
    public class AddClientCommand : CommandBase, IRequest<Guid>
    {
        // Fields that can not be null.
        public Gender? gender { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public DateOnly? birthday { get; set; }

        // Fileds that can be null.
        public bool? heart_condition { get; set; }
        public bool? diabetes { get; set; }
        public bool? hypertension { get; set; }
        public bool? bleeding_disorders { get; set; }
        public bool? immunocompromised { get; set; }
        public bool? pregnancy_status { get; set; }
        public string? allergies { get; set; }
        public string? description { get; set; }
        public string? other_conditions { get; set; }
        public string? current_medications { get; set; }
        public string? special_notes { get; set; }
    }
}