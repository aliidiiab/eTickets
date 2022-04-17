using eTickets.Data.Base;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext context;
        public MoviesService(AppDbContext _context):base(_context)
        {
            context=_context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails=await  context.Movies
                            .Include(c=>c.Cinema)
                            .Include(c => c.Producer)
                            .Include(c => c.Actors_Movies)
                            .ThenInclude(a=>a.Actor)
                            .FirstOrDefaultAsync(i=>i.Id==id);
            return movieDetails;
        }

        public async Task<NewMoviewDropDownVM> GetNewMoviewDropDownValues()
        {
            var response = new NewMoviewDropDownVM()
            {
                Actors = await context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Producers = await context.Producers.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await context.Cinemas.OrderBy(n => n.Name).ToListAsync()
            };
            return response;
        }
    }
}
