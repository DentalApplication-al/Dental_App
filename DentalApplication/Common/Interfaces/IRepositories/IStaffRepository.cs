using DentalApplication.AppointmentController.DTO;
using DentalApplication.User.StaffController.DTO;
using DentalDomain.Users.Staffs;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<Staff> GetStaffByUsername(string username);
        Task<Staff> GetStaffByEmail(string email);
        Task<bool> Exists(string email);
        Task<PaginatedResponse<ListStaff>> GetPaginatedClinicStaff(Guid staffId, Guid clinicId, int page, int take, string? search);
        Task<List<ListStaff>> GetClinicDoctors(Guid staffId, Guid clinicId);
        Task<List<Staff>> GetStaffByIdsAsync(List<Guid>? doctors);
        Task<StaffResponse> GetstaffByIdAsync(Guid staffId, Guid clinicId);
        Task<bool> Delete (Guid staffId, Guid clinicId);
        Task<Staff> GetById(Guid staffId, Guid clinicId);
        Task<PaginatedResponse<ListAppointment>> GetDoctorAppointments(Guid doctor_id, Guid clinicId, int page, int take, string? search);


    }
}
