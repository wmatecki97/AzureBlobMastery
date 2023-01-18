using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

namespace ResizeImageOnBlobUpload
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([BlobTrigger("azurefunctionblob/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
            string name, ILogger log)
        {
            using Image<Rgba32> input = Image.Load<Rgba32>(myBlob, out IImageFormat format);
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
