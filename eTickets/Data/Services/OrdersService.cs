﻿using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext context;
        public OrdersService(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId,string userrole)
        {
            var orders = await context.Orders
                .Include(n => n.OrderItems)
                .ThenInclude(n => n.Movie)
                .Include(n=>n.User)
                .ToListAsync();
            if (userrole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email =userEmailAddress,
            };
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId =item.Movie.Id,
                    OrderId=order.Id,
                    Price=item.Movie.Price
                };
                await context.OrderItems.AddAsync(orderItem);
            }
            await context.SaveChangesAsync();


        }
        public async Task clearORDERSAsync()
        {
            
            var items = await context.Orders.ToListAsync();
            foreach (var item in items)
            {
                context.Orders.Remove(item);
            }
            await context.SaveChangesAsync();

        }
       
        public async Task clearORDERSByIdAsync(int id)
        {

            var item = await context.Orders.FirstOrDefaultAsync(n=>n.Id==id);
            
            context.Orders.Remove(item);
            
            await context.SaveChangesAsync();

        }
        //public async  Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId,string userRole)
        //{
        //    var orders = await context.Orders
        //        .Include(n => n.OrderItems)
        //        .ThenInclude(n => n.Movie)
        //        .ToListAsync();
        //    if(userRole != "Admin")
        //    {
        //        orders =orders.Where(n=>n.UserId==userId).ToList();
        //    }
        //    return orders;
        //}
    }
}
