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

        public async Task<List<string>> GetAllContainerAndBlobs()
        {

            var containersAndBlobs = new List<string>();
            
            await foreach (var c in _BlobClient.GetBlobContainersAsync())
            {
                containersAndBlobs.Add("--"+c.Name);
                var client = _BlobClient.GetBlobContainerClient(c.Name);
                await foreach(var b in client.GetBlobsAsync())
                {
                    containersAndBlobs.Add("----" + b.Name);
                }
                containersAndBlobs.Add("------------------------------------------");
            }

            return containersAndBlobs;
        }
    }
}
