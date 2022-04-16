using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext context;
        public MoviesController(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await context.Movies.Include(c=>c.Cinema).ToListAsync();
            return View(allMovies);
        }
    }
}
