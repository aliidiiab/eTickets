using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly AppDbContext context;
        public ProducersController(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await context.Producers.ToListAsync();
            return View(allProducers);
        }
    }
}
