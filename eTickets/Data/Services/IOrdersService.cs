using eTickets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task clearORDERSAsync();
        Task clearORDERSByIdAsync(int id);
        Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId, string userRole);
        //Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId,string userRole);

    }
}
