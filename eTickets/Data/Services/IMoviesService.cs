using eTickets.Data.Base;
using eTickets.Models;
using eTickets.ViewModels;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMoviewDropDownVM> GetNewMoviewDropDownValues();
        Task AddNewMovieAsync(MovieViewModel newMovie);
        
        Task UpdateMovieAsync(MovieViewModel newMovie);

    }
}
