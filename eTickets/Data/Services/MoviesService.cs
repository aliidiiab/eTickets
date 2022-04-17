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
        public  async Task AddNewMovieAsync(MovieViewModel data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Descreption = data.Descreption,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaID = data.CinemaID,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerID = data.ProducerID,
            };
            await context.Movies.AddAsync(newMovie);
            await context.SaveChangesAsync();
            foreach (var actorId in data.ActorIds)
            {
                var newactormoview = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId,
                };
                await context.Actors_Movies.AddAsync(newactormoview);
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(MovieViewModel data)
        {
            var dbMovie =await context.Movies.FirstOrDefaultAsync(n=>n.Id == data.Id);
            if (dbMovie != null)
            {

                dbMovie.Name = data.Name;
                dbMovie.Descreption = data.Descreption;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaID = data.CinemaID;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerID = data.ProducerID;
                
                await context.SaveChangesAsync();
                
            }
            var exisitngActors = context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            context.Actors_Movies.RemoveRange(exisitngActors);
            await context.SaveChangesAsync();


            foreach (var actorId in data.ActorIds)
            {
                var newactormoview = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId,
                };
                await context.Actors_Movies.AddAsync(newactormoview);
            }
            await context.SaveChangesAsync();

        }
    }
}
