using DentalApplication.Common.Interfaces.IRepositories;
using DentalDomain.Appointments;
using DentalInfrastructure.Context;

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
    }
}
