using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureFunctionTangyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace AzureFunctionTangyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlobServiceClient blobServiceClient;
        static readonly HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger, BlobServiceClient blobClient)
        {
            _logger = logger;
            this.blobServiceClient = blobClient;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SalesRequest salesRequest, IFormFile file)
        {
            salesRequest.ID = Guid.NewGuid().ToString();
            using (var content = new StringContent(JsonConvert.SerializeObject(salesRequest), System.Text.Encoding.UTF8,
                "application/json"))
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost:7071/api/OnSalesUploadWriteToQueue", content);
                var returnValue = await response.Content.ReadAsStringAsync();
            }

            if (file != null)
            {
                var filename = salesRequest.ID + Path.GetExtension(file.FileName);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient("azurefunctionblob");
                var blobClient = blobContainerClient.GetBlobClient(filename);

                var headers = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                };

                await blobClient.UploadAsync(file.OpenReadStream(), headers);
            }

            return RedirectToAction(nameof(Index));
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