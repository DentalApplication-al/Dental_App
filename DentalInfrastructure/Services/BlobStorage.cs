using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using DentalApplication.Common.Interfaces.IBlobStorages;
using Microsoft.AspNetCore.Http;

namespace DentalInfrastructure.Services
{
    public class BlobStorage : IBlobStorage
    {
        const string accountName = "dentalstorage321123";
        const string accountKey = "473I4pHTp1e6vMzVL8gxOzvXM10mADQWriN3GV6uYKXxKFyPynXtlxeMCX9cl5hvDSDz7PGU+j/h+AStjIn56Q==";
        const string containerName = "filescontainer";
        private readonly BlobServiceClient _client;

        public BlobStorage()
        {
            var credintial = new StorageSharedKeyCredential(accountName, accountKey);
            var blobUri = $"https://{accountName}.blob.core.windows.net";
            _client = new BlobServiceClient(new Uri(blobUri), credintial);
        }

        public string GetLink(string path, string storedPolicyName = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "about:blank";
            }

            // Extract container name and blob name from the path
            var containerName = path.Split('/')[1];
            var blobName = path.Substring(containerName.Length + 2);

            var blobContainer = _client.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(blobName);

            // Create a SAS token that's valid for one hour
            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                BlobName = blobClient.Name,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(50)
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.Write);

            var sasUri = blobClient.GenerateSasUri(sasBuilder);
            return sasUri.ToString();
        }

        public async Task ListContainersAsync()
        {
            var containers = _client.GetBlobContainersAsync();

            await foreach (var container in containers)
            {
                Console.WriteLine(container.Name);
            }
        }

        public async Task<string> Upload(IFormFile file)
        {
            var blobContainer = _client.GetBlobContainerClient(containerName);

            var extension = Path.GetExtension(file.FileName);

            var blob = blobContainer.GetBlobClient($"{Guid.NewGuid()}{extension}");

            await blob.UploadAsync(await ConvertToStream(file));

            return blob.Uri.AbsolutePath;
        }

        public async Task<List<string>> Upload(List<IFormFile> files)
        {
            var result = new List<string>();
            foreach (var file in files)
            {
                result.Add(await Upload(file));
            }

            return result;
        }

        public async Task<bool> DeleteBlobAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            try
            {
                // Extract container name and blob name from the path
                var containerName = path.Split('/')[1];
                var blobName = path.Substring(containerName.Length + 2);

                var blobContainer = _client.GetBlobContainerClient(containerName);
                var blobClient = blobContainer.GetBlobClient(blobName);

                // Delete the blob
                await blobClient.DeleteAsync();

                return true;
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                // Blob not found
                return false;
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                Console.WriteLine($"Error deleting blob: {ex.Message}");
                return false;
            }
        }

        private async Task<Stream> ConvertToStream(IFormFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
        private async Task<List<Stream>> ConvertToStream(List<IFormFile> files)
        {
            List<Stream> result = [];
            foreach (var file in files)
            {
                result.Add(await ConvertToStream(file));
            }
            return result;
        }
    }
}
