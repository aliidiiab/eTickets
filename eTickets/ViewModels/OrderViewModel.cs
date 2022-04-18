using eTickets.Models;
using System.Collections.Generic;

namespace eTickets.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> orders { get; set; }
        public eTickets.Models.ApplicationUser userInfo { get; set; }
    }
}
