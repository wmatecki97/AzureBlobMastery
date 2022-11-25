﻿namespace AzureBlobMastery.Services
{
    public interface IBlobService
    {
        Task<string> BetBlob(string name, string containerName);
        Task<List<string>> GetAllBlobs(string containerName);
        Task<bool> UploadBlob(string name, IFormFile file, string containerName);
        Task<bool> DeleteBlob(string name, string containerName);
    }
} 
