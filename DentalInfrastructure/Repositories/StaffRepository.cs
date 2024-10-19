using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.Errors;
using DentalApplication.ServicesController.DTO;
using DentalApplication.User.StaffController.DTO;
using DentalDomain.Users.Enums;
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

        public async Task<Staff> GetById(Guid staffId, Guid clinicId)
        {
            var staff = await _context.Staffs
                .Include(a => a.StaffServices)
                .FirstOrDefaultAsync(a => a.ClinicId == clinicId && a.Id == staffId);
            return staff;
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

        public async Task<PaginatedResponse<ListAppointment>> GetDoctorAppointments(Guid doctor_id, Guid clinicId, int page, int take, string? search, bool history)
        {
            if (history) 
            {
                return await GetDoctorPastAppointments(doctor_id, clinicId, page, take, search);
            }
            else
            {
                return await GetDoctorUpcomingAppointments(doctor_id, clinicId, page, take, search);
            }
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
                    working_hours = $"{a.StartTime} - {a.EndTime}",
                    status = a.Status,
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

        public async Task<StaffResponse> GetstaffByIdAsync(Guid staffId, Guid clinicId)
        {
            var staff = await _context.Staffs
                .Where(a => a.Id == staffId && a.ClinicId == clinicId)
                .Select(a => new StaffResponse
                {
                    birthday = a.Birthday,
                    email = a.Email,
                    end_time = a.EndTime,
                    first_name = a.FirstName,
                    last_name = a.LastName,
                    job_type = a.JobType,
                    phone = a.Phone,
                    role = a.Role,
                    id = a.Id,
                    picture = a.ProfilePic,
                    start_time = a.StartTime,
                    created_at = $"{a.CreatedOn.Day}-{a.CreatedOn.Month}-{a.CreatedOn.Year}",
                    status = a.Status,
                   services = a.StaffServices.Select(b => new ListService
                   {
                       id = b.Id,
                       name = b.Name,
                   }).ToList()
                }).FirstOrDefaultAsync();
            return staff;
        }

        public async Task<List<Staff>> GetStaffByIdsAsync(List<Guid>? doctors)
        {
            var staff = await _context.Staffs
                .Include(a => a.StaffServices)
                .Where(a => doctors.Contains(a.Id) && a.Role == Role.DOCTOR)
                .ToListAsync();
            return staff;
        }

        public async Task<Staff> GetStaffByUsername(string username)
        {
            return await _context.Staffs.FirstOrDefaultAsync(a => a.Email == username);
        }

        private async Task<PaginatedResponse<ListAppointment>> GetDoctorUpcomingAppointments(Guid doctor_id, Guid clinicId, int page, int take, string? search)
        {
            var date = DateTime.Now;

            var totalElements = await _context.Staffs
                .Where(a => a.ClinicId == clinicId && a.Id == doctor_id)
                .Include(a => a.Appointments.Where(a => a.StartDate > date))
                .SelectMany(a => a.Appointments.Where(a => a.StartDate > date))
                .CountAsync();

            var totalPages = (int)Math.Ceiling(totalElements / (double)take);

            var appointments = await _context.Staffs
                .Where(a => a.Id == doctor_id && a.ClinicId == clinicId)
                .Include(a => a.Appointments.Where(a => a.StartDate > date)).ThenInclude(a => a.Client)
                .SelectMany(a => a.Appointments.Where(a => a.StartDate > date))
                .Skip((page - 1) * take)
                .Take(take)
                .Select(b => new ListAppointment
                {
                    client = $"{b.Client.FirstName} {b.Client.LastName}",
                    id = b.Id,
                    //doctors = b.Doctor.FirstName,
                    treatment = b.Service.Name,
                    date = $"{b.StartDate.Day}-{b.StartDate.Month}-{b.StartDate.Year}",
                    time = $"{b.StartDate:HH:mm} - {b.EndDate:HH:mm}",
                }).ToListAsync();

            var result = new PaginatedResponse<ListAppointment>
            {
                data = appointments,
                totalPages = totalPages,
                pageNumber = page,
                totalElements = totalElements,
                pageSize = take,
            };
            return result;
        }
        private async Task<PaginatedResponse<ListAppointment>> GetDoctorPastAppointments(Guid doctor_id, Guid clinicId, int page, int take, string? search)
        {
            var date = DateTime.Now;

            var totalElements = await _context.Staffs
                .Where(a => a.ClinicId == clinicId && a.Id == doctor_id)
                .Include(a => a.Appointments.Where(a => a.StartDate < date))
                .SelectMany(a => a.Appointments.Where(a => a.StartDate < date))
                .CountAsync();

            var totalPages = (int)Math.Ceiling(totalElements / (double)take);

            var appointments = await _context.Staffs
                .Where(a => a.Id == doctor_id && a.ClinicId == clinicId)
                .Include(a => a.Appointments.Where(a => a.StartDate < date)).ThenInclude(a => a.Client)
                .SelectMany(a => a.Appointments.Where(a => a.StartDate < date))
                .Skip((page - 1) * take)
                .Take(take)
                .Select(b => new ListAppointment
                {
                    client = $"{b.Client.FirstName} {b.Client.LastName}",
                    id = b.Id,
                    //doctors = b.Doctor.FirstName,
                    treatment = b.Service.Name,
                    date = $"{b.StartDate.Day}-{b.StartDate.Month}-{b.StartDate.Year}",
                    time = $"{b.StartDate:HH:mm} - {b.EndDate:HH:mm}",
                }).ToListAsync();

            var result = new PaginatedResponse<ListAppointment>
            {
                data = appointments,
                totalPages = totalPages,
                pageNumber = page,
                totalElements = totalElements,
                pageSize = take,
            };
            return result;
        }

        public async Task<List<Staff>> GetDoctorsById(List<Guid> ids, Guid clinicId)
        {
            var doctors = await _context.Staffs
                .Where(a => ids.Contains(a.Id) && a.ClinicId == clinicId)
                .ToListAsync();

            return doctors;
        }
    }
}
