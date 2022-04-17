using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService service;
        public MoviesController(IMoviesService _service)
        {
            service = _service;
            
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await service.GetAllAsync(n=>n.Cinema);
            return View(allMovies);
        }
        public async Task<IActionResult> Details(int id)
        {
            var moviedetail= await service.GetMovieByIdAsync(id);
            return View(moviedetail);
        }
        public async Task<IActionResult> Create()
        {
            var movieDropDownsData = await service.GetNewMoviewDropDownValues();
            ViewBag.CinemaId = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
            ViewBag.ProducerId = new SelectList(movieDropDownsData.Producers, "Id", "FullName");
            ViewBag.ActorId = new SelectList(movieDropDownsData.Actors, "Id", "FullName");

            return View();
        }
        [HttpPost]
        public IActionResult Create(int id)
        {
            //ViewBag.Cinemas= 
            return View();
        }
    }
}
