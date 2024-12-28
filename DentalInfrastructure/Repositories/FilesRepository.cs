using DentalApplication.Common.Interfaces.IRepositories;
using DentalDomain.Files;
using DentalInfrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DentalInfrastructure.Repositories
{
    public class FilesRepository : GenericRepository<Documents>, IFilesRepository
    {
        public FilesRepository(DentalContext context) : base(context)
        {
        }

        public async Task<bool> AddRange(List<Documents> documents)
        {
            await _context.AddRangeAsync(documents);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(List<Documents> filesToDelete)
        {
            try
            {
                _context.RemoveRange(filesToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Documents>> GetClientFiles(Guid clientId, Guid clinicId, bool images)
        {
            var query = _context.Files
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.ClientId == clientId && x.ClinicId == clinicId);

            if(images)
            {
                return await query.Where(x => x.IsImage)
                    .ToListAsync();
            }
            else
            {
                return await query.Where(x => !x.IsImage)
                    .ToListAsync();
            }
        }
    }
}
