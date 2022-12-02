using AzureBlobMastery.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobMastery.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobService blobService;

        public BlobController(IBlobService blobService)
        {
            this.blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageAsync(string containerName)
        {
            var blobs = await blobService.GetAllBlobs(containerName);
            return View(blobs);
        }

        [HttpGet]
        public IActionResult AddFile(string containerName)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file, string containerName)
        {
            if (file is null || file.Length < 1)
            {
                return View();
            }

            var filename = Path.GetFileNameWithoutExtension(file.Name)+Guid.NewGuid()+Path.GetExtension(file.FileName);
            var result = await blobService.UploadBlob(filename, file, containerName);

            if (result)
                return RedirectToAction("Index", "Container");

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ViewFile(string name, string containerName)
        {
            return Redirect(await blobService.GetBlob(name, containerName));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFile(string name, string containerName)
        {
            await blobService.DeleteBlob(name, containerName);
            return RedirectToAction("Index", "Home");
        }

    }
}
