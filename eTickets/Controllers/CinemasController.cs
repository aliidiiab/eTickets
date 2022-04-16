using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly AppDbContext context;
        public CinemasController(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await context.Cinemas.ToListAsync();
            return View(allCinemas);
        }
    }
}
