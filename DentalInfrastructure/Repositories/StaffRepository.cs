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

        public async Task<bool> Exists(string email)
        {
            var exists = await _context.Staffs.AnyAsync(a => a.Email.ToLower() == email.ToLower());
            return exists;
        }

        public async Task<List<Staff>> GetClinicStaff(Guid staffId, Guid clinicId)
        {
            var staff = await _context.Staffs.Where(a => a.ClinicId == clinicId && a.Id != staffId).ToListAsync();
            return staff;
        }

        public async Task<Staff> GetStaffByEmail(string username)
        {
            return await _context.Staffs.FirstOrDefaultAsync(a => a.Email == username);
        }

        public async Task<Staff> GetstaffByIdAsync(Guid staffId, Guid clinicId)
        {
            return await _context.Staffs.FirstOrDefaultAsync(a => a.Id == staffId && a.ClinicId == clinicId);
        }

        public async Task<List<Staff>> GetStaffByIdsAsync(List<Guid>? doctors)
        {
            var staff = await _context.Staffs
                .Where(a => doctors.Contains(a.Id))
                .ToListAsync();
            return staff;
        }

        public async Task<Staff> GetStaffByUsername(string username)
        {
            return await _context.Staffs.FirstOrDefaultAsync(a => a.Email == username);
        }

    }
}
