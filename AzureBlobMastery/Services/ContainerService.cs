using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobMastery.Services
{
    public class ContainerService : IContainerService
    {
        public ContainerService(BlobServiceClient blobClient)
        {
            _BlobClient = blobClient;
        }

        private BlobServiceClient _BlobClient { get; }

        public async Task CreateContainer(string containerName)
        {
            BlobContainerClient bcc = _BlobClient.GetBlobContainerClient(containerName);
            await bcc.CreateIfNotExistsAsync(PublicAccessType.Blob);
        }

        public async Task DeleteContainer(string containerName)
        {
            BlobContainerClient bcc = _BlobClient.GetBlobContainerClient(containerName);
            await bcc.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainer()
        {
            List<string> containerNames = new();

            await foreach (BlobContainerItem blobContainerItem in _BlobClient.GetBlobContainersAsync())
            {
                containerNames.Add(blobContainerItem.Name);
            }

            return containerNames;
        }

        public Task<List<string>> GetAllContainerAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}
