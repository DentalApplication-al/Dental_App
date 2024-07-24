using DentalDomain.Users.Staffs;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<Staff> GetStaffByUsername(string username);
        Task<bool> Exists(string username, string email);
    }
}
