using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Create(MovieViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropDownsData = await service.GetNewMoviewDropDownValues();
                ViewBag.CinemaId = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
                ViewBag.ProducerId = new SelectList(movieDropDownsData.Producers, "Id", "FullName");
                ViewBag.ActorId = new SelectList(movieDropDownsData.Actors, "Id", "FullName");
                return View(movie);
            }
            await service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var moviedetails = await service.GetMovieByIdAsync(id);
            if(moviedetails == null) return View("NotFound");
            var response = new MovieViewModel()
            {
                Id = moviedetails.Id,
                Name = moviedetails.Name,
                Descreption = moviedetails.Descreption,
                Price = moviedetails.Price,
                ImageURL = moviedetails.ImageURL,
                MovieCategory = moviedetails.MovieCategory,
                CinemaID = moviedetails.CinemaID,
                ProducerID = moviedetails.ProducerID,
                StartDate = moviedetails.StartDate,
                EndDate = moviedetails.EndDate,
                ActorIds = moviedetails.Actors_Movies.Select(n=>n.ActorId).ToList()
                
            };
            var movieDropDownsData = await service.GetNewMoviewDropDownValues();
            ViewBag.CinemaId = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
            ViewBag.ProducerId = new SelectList(movieDropDownsData.Producers, "Id", "FullName");
            ViewBag.ActorId = new SelectList(movieDropDownsData.Actors, "Id", "FullName");

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,MovieViewModel movie)
        {
            if(id != movie.Id) return View("NotFound");

            
            if (!ModelState.IsValid)
            {
                var movieDropDownsData = await service.GetNewMoviewDropDownValues();
                ViewBag.CinemaId = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
                ViewBag.ProducerId = new SelectList(movieDropDownsData.Producers, "Id", "FullName");
                ViewBag.ActorId = new SelectList(movieDropDownsData.Actors, "Id", "FullName");
                return View(movie);
            }
            await service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var moviedetails = await service.GetMovieByIdAsync(id);
            if (moviedetails == null) return View("NotFound");
            await service.DeleteMovieAsync(id);

            return RedirectToAction(nameof(Index));
        }
            public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await service.GetAllAsync(n => n.Cinema);
            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredMovies = allMovies.Where(n => n.Name.Contains(searchString)
                                                       || n.Descreption.Contains(searchString)).ToList();
               
                return View("Index",filteredMovies);
            }
            
            
            return View("Index",allMovies);
        }


    }
}
