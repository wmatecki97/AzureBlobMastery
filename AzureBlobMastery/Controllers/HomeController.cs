using AzureBlobMastery.Models;
using AzureBlobMastery.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureBlobMastery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContainerService cs;
        private readonly IBlobService blobService;

        public HomeController(IContainerService cs, IBlobService blobService)
        {
            this.cs = cs;
            this.blobService = blobService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var containersAndBlobs = await cs.GetAllContainerAndBlobs();
            return View(containersAndBlobs);
        }

        public async Task<IActionResult> Images()
        {
            return View(await blobService.GetAllBlobsWithUri("dotnetmastery-images"));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}