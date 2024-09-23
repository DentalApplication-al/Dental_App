using Microsoft.AspNetCore.Http;

namespace DentalApplication.Common.Interfaces.IBlobStorages
{
    public interface IBlobStorage
    {
        Task ListContainersAsync();
        Task<BlobResponse> Upload(IFormFile file);
        Task<List<string>> Upload(List<IFormFile> files);
        string GetLink(string path, string storedPolicyName = null);
        Task<bool> DeleteBlobAsync(string path);
        Task<bool> DeleteBlobAsync(List<string> paths);
    }
}
