using DentalApplication.User.StaffController.DTO;
using DentalDomain.Services;

namespace DentalApplication.ServicesController
{
    public class ServiceResponse
    {
        public Guid id { get; set; }
        public decimal? price { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? duration { get; set; }
        public DateTime? created_on { get; set; }
        public DateTime? updated_on { get; set; }
        public Guid? clinic_id { get; set; }
        public List<StaffResponse> services_staff { get; set; } = new();
        private ServiceResponse(Service service)
        {
            id = service.Id;
            price = service.Price;
            name = service.Name;
            description = service.Description;
            duration = service.Duration;
            created_on = service.CreatedOn;
            updated_on = service.UpdatedOn;
            clinic_id = service.ClinicId;
            services_staff = StaffResponse.Map(service.ServiceStaff);
        }

        public static ServiceResponse Map(Service service)
        {
            service ??= Service.Create();
            return new(service);
        }
        public static List<ServiceResponse> Map(List<Service> services)
        {
            services ??= [];

            return services.Select(services => Map(services)).ToList();
        }

    }
}
