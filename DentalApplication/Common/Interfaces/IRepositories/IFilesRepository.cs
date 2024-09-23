using DentalDomain.Files;
using System.Reflection.Metadata;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IFilesRepository : IGenericRepository<Documents>
    {
        Task<bool> AddRange(List<Documents> documents);
        Task<bool> DeleteAsync(List<Documents> filesToDelete);
    }
}
