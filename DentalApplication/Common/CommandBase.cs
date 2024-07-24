using System.Text.Json.Serialization;

namespace DentalApplication.Common
{
    public abstract class CommandBase
    {
        [JsonIgnore]
        public Guid clinic_id { get; set; }
    }
}
