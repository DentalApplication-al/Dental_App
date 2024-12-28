using DentalApplication.AppointmentController.DTO;
using DentalDomain.Appointments;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
        Task<bool> IsDoctorAvailable(Guid doctorId, Guid clinic, DateTime start, DateTime end);
        Task<PaginatedResponse<ListAppointment>> GetPaginatedAppointments(Guid clinicId, int take, int page, string? search);
    }
}
