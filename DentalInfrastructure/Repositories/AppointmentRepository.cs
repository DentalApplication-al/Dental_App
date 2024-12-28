using DentalApplication.AppointmentController.DTO;
using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalDomain.Appointments;
using DentalInfrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DentalInfrastructure.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DentalContext context) : base(context)
        {
        }

        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return appointment;
        }

        public async Task<PaginatedResponse<ListAppointment>> GetPaginatedAppointments(Guid clinicId, int take, int page, string? search)
        {
            search = search ?? "";
            var basicQuery = _context.Appointments
                .Where(a => a.ClinicId == clinicId && (a.Client.FirstName.Contains(search) || a.Doctor.Any(b => b.FirstName.Contains(search))));

            var totalElements = await basicQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(totalElements / (double)take);

            var appointments = await basicQuery
                .OrderByDescending(a => a.CreatedOn)
                .Skip((page - 1) * take)
                .Take(take)
                .Include(a => a.Doctor)
                .Include(a => a.Client)
                .Include(a => a.Service)
                .Select(a => new ListAppointment
                {
                    client = $"{a.Client.FirstName} {a.Client.LastName}",
                    date = $"{a.StartDate.Day}-{a.StartDate.Month}-{a.StartDate.Year}",
                    id =a.Id,
                    treatment = a.Service.Name,
                    time = $"{a.StartDate:HH:mm}-{a.EndDate:HH:mm}",
                    startTime = $"{a.StartDate:HH:mm}", 
                    endTime = $"{a.EndDate:HH:mm}",
                    doctors = string.Join(",", a.Doctor.Select(d => $"{d.FirstName} {d.LastName}").ToList())
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

        public async Task<bool> IsDoctorAvailable(Guid doctorId, Guid clinicId, DateTime start, DateTime end)
        {
            var appointments = await _context.Appointments
                .Where(a => a.Doctor.Any(d => d.Id == doctorId) && a.ClinicId == clinicId)
                .ToListAsync();

            // Check if any appointment overlaps with the requested time range
            var isAvailable = !appointments.Any(a =>
                // Check for time overlap
                (a.StartDate < end && a.EndDate > start)
            );
            return isAvailable;

        }
    }
}
