namespace AzureBlobMastery.Services
{
    public interface IBlobService
    {
        Task<string> BetBlob(string name, string containerName);
        Task<List<string>> GetAllBlobs(string containerName);
        Task UploadBlob(string name, IFormFile file, string containerName);
        Task DeleteBlob(string name, string containerName);
    }
} 
