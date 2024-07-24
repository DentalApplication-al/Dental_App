using DentalDomain.Users.SuperAdmin;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface ISuperAdminRepository : IGenericRepository<SuperAdmin>
    {
        Task<SuperAdmin> GetSuperAdminByUsername(string username);
    }
}
