using System.ComponentModel.DataAnnotations;

namespace DentalDomain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid ClinicId { get; set; }

    }
}
