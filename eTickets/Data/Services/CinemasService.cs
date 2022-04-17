using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class CinemasService:EntityBaseRepository<Cinema>,IcinemasService
    {
        public CinemasService(AppDbContext context):base(context)
        {

        }
    }
}
