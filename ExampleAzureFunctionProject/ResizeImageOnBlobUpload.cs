using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ExampleAzureFunctionProject
{
    public class ResizeImageOnBlobUpload
    {
        [FunctionName("ResizeImageOnBlobUpload")]
        public void Run([BlobTrigger("azurefunctionblob/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
            [Blob("azurefunctionblob-sm/{name}", FileAccess.Write)] Stream blobOutputStream,
            string name, ILogger log)
        {
            using Image<Rgba32> input = Image.Load<Rgba32>(myBlob, out IImageFormat format);
            input.Mutate(x => x.Resize(300, 200));
            input.Save(blobOutputStream, format);
        }
    }
}
