using DentalDomain.Files;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IFilesRepository : IGenericRepository<Documents>
    {
        Task<bool> AddRange(List<Documents> documents);
        Task<bool> DeleteAsync(List<Documents> filesToDelete);
        Task<List<Documents>> GetClientFiles(Guid clientId, Guid clinicId, bool images);
    }
}
