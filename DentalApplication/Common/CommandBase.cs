using DentalApplication.Swagger;

namespace DentalApplication.Common
{
    public abstract class CommandBase
    {
        [SwaggerIgnore]
        public Guid? clinic_id { get; set; }

        [SwaggerIgnore]
        public Guid? loged_in_staff_id { get; set; }
    }
}
