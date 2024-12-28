
using DentalDomain.Files;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IDocumentRepository : IGenericRepository<Documents>
    {
        Task<List<Documents>> GetClientDocuments(Guid clientId, Guid clinicId);
    }
}
