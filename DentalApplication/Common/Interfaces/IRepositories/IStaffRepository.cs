using DentalDomain.Users.Staffs;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<Staff> GetStaffByUsername(string username);
        Task<Staff> GetStaffByEmail(string username);
        Task<bool> Exists(string email);
        Task<List<Staff>> GetClinicStaff(Guid staffId, Guid clinicId);
        Task<List<Staff>> GetStaffByIdsAsync(List<Guid>? doctors);
        Task<Staff> GetstaffByIdAsync(Guid staffId, Guid clinicId);
    }
}
