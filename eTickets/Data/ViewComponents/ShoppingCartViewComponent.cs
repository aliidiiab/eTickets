using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Data.ViewComponents
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart shoppingCart;
        public ShoppingCartSummary(ShoppingCart _shoppingCart)
        {
            shoppingCart =_shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var items=shoppingCart.GetShoppingCartItems();
            return View(items.Count);
        }
    }
}
