using AzureBlobMastery.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobMastery.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainerService containerSErvice;

        public ContainerController(IContainerService containerSErvice)
        {
            this.containerSErvice = containerSErvice;
        }

        public async Task<IActionResult> Index()
        {
            var allContainers = await containerSErvice.GetAllContainer();
            return View(allContainers);
        }
    }
}
