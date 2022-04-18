using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly IcinemasService service;
        public CinemasController(IcinemasService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await service.GetAllAsync());
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View(new Cinema());
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            Cinema cinema = await service.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");
            await service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            Cinema cinema = await service.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            Cinema cinemadetails = await service.GetByIdAsync(id);
            if (cinemadetails == null) return View("NotFound");
            return View(cinemadetails);

        }
    }
}
