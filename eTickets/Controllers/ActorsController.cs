using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService service;
        public ActorsController(IActorsService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await service.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View(new Actor());
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }
            await service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            Actor actordetails=await service.GetByIdAsync(id);
            if (actordetails == null) return View("NotFound");
            return View(actordetails);
            
        }
        public async Task<IActionResult> Edit(int id)
        {
            Actor actordetails = await service.GetByIdAsync(id);
            if (actordetails == null) return View("NotFound");
            return View(actordetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await service.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Actor actordetails = await service.GetByIdAsync(id);
            if (actordetails == null) return View("NotFound");
            await service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
