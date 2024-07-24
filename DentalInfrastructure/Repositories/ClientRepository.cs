using DentalApplication.Common.Interfaces.IRepositories;
using DentalDomain.Users.Clients;
using DentalInfrastructure.Context;

namespace DentalInfrastructure.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DentalContext context) : base(context)
        {
        }
    }
}
