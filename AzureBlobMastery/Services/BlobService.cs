using Azure.Storage.Blobs;

namespace AzureBlobMastery.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobClient;

        public BlobService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<string> BetBlob(string name, string containerName)
        {
            var client = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = client.GetBlobClient(name);

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<bool> DeleteBlob(string name, string containerName)
        {
            var client = _blobClient.GetBlobContainerClient(containerName);
            var res = await client.DeleteBlobIfExistsAsync(name);
            return res.Value;
        }

        public async Task<List<string>> GetAllBlobs(string containerName)
        {
            var client = _blobClient.GetBlobContainerClient(containerName);
            var blobs = client.GetBlobsAsync();
            var blobStrings = new List<string>();
            await foreach(var blob in blobs)
            {
                blobStrings.Add(blob.Name);
            }

            return blobStrings;
        }

        public async Task<bool> UploadBlob(string name, IFormFile file, string containerName)
        {
            var client = _blobClient.GetBlobContainerClient(containerName);
            var result = await client.UploadBlobAsync(name, file.OpenReadStream());
            return result is null ? false : true;
        }
    }
}
