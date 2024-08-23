using DentalApplication.User.StaffController;
using DentalDomain.Users.Staffs;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<Staff> GetStaffByUsername(string username);
        Task<Staff> GetStaffByEmail(string username);
        Task<bool> Exists(string email);
        Task<PaginatedResponse<ListStaff>> GetPaginatedClinicStaff(Guid staffId, Guid clinicId, int page, int take);
        Task<List<Staff>> GetStaffByIdsAsync(List<Guid>? doctors);
        Task<Staff> GetstaffByIdAsync(Guid staffId, Guid clinicId);
    }
}
