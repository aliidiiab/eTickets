using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext context;
        public ActorsService(AppDbContext _context)
        {
            context = _context;
        }
        public async Task AddAsync(Actor actor)
        {
            await context.Actors.AddAsync(actor);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {        
            context.Actors.Remove(await GetByIdAsync(id));
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await context.Actors.ToListAsync();  
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            var result =await context.Actors.FirstOrDefaultAsync(i=>i.Id == id);
            return result;
        }

        public async Task<Actor> UpdateAsync(int id, Actor newActor)
        {
            context.Update(newActor);
            await context.SaveChangesAsync();
            return newActor;
        }
    }
}
