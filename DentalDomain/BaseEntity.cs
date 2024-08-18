using System.ComponentModel.DataAnnotations;

namespace DentalDomain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public DateTime UpdatedOn { get; protected set; }
        public Guid ClinicId { get; protected set; }

    }
}
