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

        public Task<string> BetBlob(string name, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBlob(string name, string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllBlobs(string containerName)
        {
            throw new NotImplementedException();
        }

        public Task UploadBlob(string name, IFormFile file, string containerName)
        {
            throw new NotImplementedException();
        }
    }
}
