using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService service;
        public ProducersController(IProducersService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await service.GetAllAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            Producer producerdetails = await service.GetByIdAsync(id);
            if (producerdetails == null) return View("NotFound");
            return View(producerdetails);

        }
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View(new Producer());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            Producer producerdetails = await service.GetByIdAsync(id);
            if (producerdetails == null) return View("NotFound");
            await service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            Producer producerdetails = await service.GetByIdAsync(id);
            if (producerdetails == null) return View("NotFound");
            return View(producerdetails);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await service.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }
    }
}
