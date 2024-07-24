using DentalApplication.Common.Interfaces.IRepositories;
using DentalDomain.Users.Staffs;
using DentalInfrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DentalInfrastructure.Repositories
{
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        public StaffRepository(DentalContext context) : base(context)
        {
        }

        public async Task<bool> Exists(string username, string email)
        {
            var exists = await _context.Staffs.AnyAsync(a => a.Username == username || a.Email == email);
            return exists;
        }

        public async Task<Staff> GetStaffByUsername(string username)
        {
            return await _context.Staffs.FirstOrDefaultAsync(a => a.Username == username);
        }
    }
}
