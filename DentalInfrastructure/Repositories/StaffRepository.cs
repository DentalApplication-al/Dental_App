using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.User.StaffController.DTO;
using DentalDomain.Users.Enums;
using DentalDomain.Users.Staffs;
using DentalInfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DentalInfrastructure.Repositories
{
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        public StaffRepository(DentalContext context) : base(context)
        {
        }

        public async Task<bool> Delete(Guid staffId, Guid clinicId)
        {
            var staff = await _context.Staffs.Where(a => a.Id == staffId && a.ClinicId == clinicId).FirstOrDefaultAsync() ?? 
                throw new NotFoundException("Staff could not be found.");
            try
            {
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> Exists(string email)
        {
            var exists = await _context.Staffs.AnyAsync(a => a.Email.ToLower() == email.ToLower());
            return exists;
        }

        public async Task<List<ListStaff>> GetClinicDoctors(Guid staffId, Guid clinicId)
        {
            var doctors = await _context.Staffs
                .Where(a => a.ClinicId == clinicId && a.Role == Role.DOCTOR)
                .Select(a => new ListStaff
                {
                    id = a.Id,
                    first_name = a.FirstName,
                    last_name = a.LastName,
                }).ToListAsync();
            return doctors;
        }

        public async Task<PaginatedResponse<ListStaff>> GetPaginatedClinicStaff(Guid staffId, Guid clinicId, int page, int take, string? search)
        {
            // Calculate the total number of elements
            var totalElements = await _context.Staffs.CountAsync(a => a.ClinicId == clinicId && a.Id != staffId && a.FirstName.Contains(search?? ""));

            // Calculate the total number of pages
            var totalPages = (int)Math.Ceiling(totalElements / (double)take);

            // Fetch the paginated staff list
            var staff = await _context.Staffs
                .Where(a => a.ClinicId == clinicId && a.Id != staffId && a.FirstName.Contains(search ?? ""))
                .Skip((page - 1) * take)
                .Take(take)
                .Select(a => new ListStaff
                {
                    id = a.Id,
                    first_name = a.FirstName,
                    last_name = a.LastName,
                    job_type = a.JobType,
                    phone = a.Phone,
                    role = a.Role,
                    working_hours = $"{a.StartTime} - {a.EndTime}"
                }).ToListAsync();

            // Populate the PaginatedResponse object
            var result = new PaginatedResponse<ListStaff>
            {
                data = staff,
                pageNumber = page,
                pageSize = take,
                totalElements = totalElements,
                totalPages = totalPages
            };

            return result;
        }

        public async Task<Staff> GetStaffByEmail(string email)
        {
            return await _context.Staffs.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Staff> GetstaffByIdAsync(Guid staffId, Guid clinicId)
        {
            return await _context.Staffs.FirstOrDefaultAsync(a => a.Id == staffId && a.ClinicId == clinicId);
        }

        public async Task<List<Staff>> GetStaffByIdsAsync(List<Guid>? doctors)
        {
            var staff = await _context.Staffs
                .Include(a => a.StaffServices)
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
