using System.Text.Json.Serialization;

namespace DentalApplication.Common
{
    public abstract class CommandBase
    {
        [JsonIgnore]
        public Guid clinic_id { get; set; }

        [JsonIgnore]
        public Guid loged_in_staff_id { get; set; }
    }
}
