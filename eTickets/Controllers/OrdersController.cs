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
        public OrdersController(IMoviesService _moviesservice, ShoppingCart _shoppingCart)
        {
            moviesservice = _moviesservice;
            shoppingCart = _shoppingCart;
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
    }
}
