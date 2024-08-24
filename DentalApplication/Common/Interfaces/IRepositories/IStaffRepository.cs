using DentalApplication.User.StaffController;
using DentalDomain.Users.Staffs;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<Staff> GetStaffByUsername(string username);
        Task<Staff> GetStaffByEmail(string email);
        Task<bool> Exists(string email);
        Task<PaginatedResponse<ListStaff>> GetPaginatedClinicStaff(Guid staffId, Guid clinicId, int page, int take, string? search);
        Task<List<Staff>> GetStaffByIdsAsync(List<Guid>? doctors);
        Task<Staff> GetstaffByIdAsync(Guid staffId, Guid clinicId);
        Task<bool> Delete (Guid staffId, Guid clinicId);
    }
}
