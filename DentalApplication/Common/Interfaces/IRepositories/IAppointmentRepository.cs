using DentalDomain.Appointments;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
    }
}
