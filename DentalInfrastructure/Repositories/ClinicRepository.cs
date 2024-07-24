using DentalApplication.Common.Interfaces.IRepositories;
using DentalDomain.Clinics;
using DentalInfrastructure.Context;

namespace DentalInfrastructure.Repositories
{
    public class ClinicRepository : GenericRepository<Clinic>, IClinicRepository
    {
        public ClinicRepository(DentalContext context) : base(context)
        {
        }
    }
}
