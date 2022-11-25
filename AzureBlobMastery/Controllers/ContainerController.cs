using AzureBlobMastery.Models;
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

        public async Task<IActionResult> Delete(string id)
        {
            await containerSErvice.DeleteContainer(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View(new Container());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Container container)
        {
            await containerSErvice.CreateContainer(container.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}
