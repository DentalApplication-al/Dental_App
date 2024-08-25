using DentalApplication.Common.Interfaces.IBlobStorages;
using DentalApplication.ServicesController.DTO;
using DentalDomain.Users.Enums;
using DentalDomain.Users.Staffs;
using Microsoft.AspNetCore.Http;

namespace DentalApplication.User.StaffController.DTO
{
    public class StaffResponse
    {
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string? phone { get; set; }
        public Role? role { get; set; }
        public DateOnly? birthday { get; set; }
        public string? job_type { get; set; }
        public string? picture { get; set; }
        public string? start_time { get; set; }
        public string? end_time { get; set; }
        List<ServiceResponse> services { get; set; }
        private StaffResponse(Staff staff)
        {
            id = staff.Id;
            first_name = staff.FirstName;
            last_name = staff.LastName;
            email = staff.Email;
            phone = staff.Phone;
            role = staff.Role;
            birthday = staff.Birthday;
            job_type = staff.JobType;
            picture = "Link";
            start_time = staff.StartTime;
            end_time = staff.EndTime;
            //services = ServiceResponse.Map(staff.StaffServices);
        }
        public static StaffResponse Map(Staff staff)
        {
            return new StaffResponse(staff);
        }
        public static List<StaffResponse> Map(List<Staff> staffs)
        {
            return staffs.Select(a => new StaffResponse(a)).ToList();
        }
        public StaffResponse()
        {

        }
    }
}
