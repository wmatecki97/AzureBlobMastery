using AzureBlobMastery.Models;
using AzureBlobMastery.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureBlobMastery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContainerService cs;

        public HomeController(IContainerService cs)
        {
            this.cs = cs;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var containersAndBlobs = await cs.GetAllContainerAndBlobs();
            return View(containersAndBlobs);
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