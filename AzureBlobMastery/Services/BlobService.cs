using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using AzureBlobMastery.Models;

namespace AzureBlobMastery.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobClient;

        public BlobService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<string> GetBlob(string name, string containerName)
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
            await foreach (var blob in blobs)
            {
                blobStrings.Add(blob.Name);
            }

            return blobStrings;
        }

        public async Task<bool> UploadBlob(string name, IFormFile file, string containerName, Blob blob)
        {
            var client = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = client.GetBlobClient(name);

            IDictionary<string, string> metadata = new Dictionary<string, string>()
            {
                ["title"] = blob.Title,
                ["comment"] = blob.Comment
            };
            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };

            var result = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders, metadata);



            return result is null ? false : true;
        }

        public async Task<List<Blob>> GetAllBlobsWithUri(string containerName)
        {
            var client = _blobClient.GetBlobContainerClient(containerName);
            var blobs = client.GetBlobsAsync();
            var blobList = new List<Blob>();
            string sasContainerSignature = string.Empty;
            if (client.CanGenerateSasUri)
            {
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = client.Name,
                    Resource = "c",
                };
                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1);
                sasBuilder.SetPermissions(BlobSasPermissions.Read);
                sasContainerSignature = client.GenerateSasUri(sasBuilder).AbsoluteUri;
                sasContainerSignature = sasContainerSignature.Split('?')[1].ToString();
            }

            await foreach (var item in blobs)
            {
                var blobClient = client.GetBlobClient(item.Name);
                Blob blobIndividual = new()
                {
                    Uri = blobClient.Uri.AbsoluteUri + $"?{sasContainerSignature}",
                };
               
                var properties = await blobClient.GetPropertiesAsync();
                if (properties.Value.Metadata.TryGetValue("title", out var title))
                {
                    blobIndividual.Title = title;
                }

                if (properties.Value.Metadata.TryGetValue("comment", out var comment))
                {
                    blobIndividual.Title = comment;
                }
                blobList.Add(blobIndividual);
            }

            return blobList;
        }
    }
}
