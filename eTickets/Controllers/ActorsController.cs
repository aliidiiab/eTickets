using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly AppDbContext context;
        public ActorsController(AppDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            var data = context.Actors.ToList();
            return View(data);
        }
    }
}
