using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService moviesservice;
        private readonly ShoppingCart shoppingCart;
        private readonly IOrdersService orderservice;


        public OrdersController(IOrdersService _orderservice,IMoviesService _moviesservice, ShoppingCart _shoppingCart)
        {
            moviesservice = _moviesservice;
            shoppingCart = _shoppingCart;
            orderservice = _orderservice;
        }
        public async Task<IActionResult> Index()
        {
            string userId = ""; 
            var orders =  await orderservice.GetOrderByUserIdAsync(userId);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items =shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems= items;
            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal =shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item =await  moviesservice.GetMovieByIdAsync(id);
            if (item != null)
            {
                shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await moviesservice.GetMovieByIdAsync(id);
            if (item != null)
            {
                shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> completeOrder()
        {
            var items=shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";

            await orderservice.StoreOrderAsync(items, userId, userEmailAddress);
            await shoppingCart.clearShoppingCartAsync();
            return View(items);
        }
        public async Task<IActionResult> clearOrder()
        {
            await orderservice.clearORDERSAsync();
            //await shoppingCart.clearShoppingCartAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> clearOrderById(int id)
        {
            await orderservice.clearORDERSByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
