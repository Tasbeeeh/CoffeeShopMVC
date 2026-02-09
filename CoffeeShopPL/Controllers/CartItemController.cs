using CoffeeShopBLL.ModelVMs.CartItem;
using CoffeeShopBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopPL.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        public IActionResult Index(int cartId)
        {
           var items= _cartItemService.GetItems(cartId).ToList();
           return View("Index", items);
        }
       
        [HttpPost]
        public IActionResult Add(int cartId, AddItemVM vm)
        {
            _cartItemService.AddItem(cartId, vm);
            return RedirectToAction("Details", "Cart", new { cartId });
        }
        [HttpPost]
        public IActionResult Update(int cartId,int productId ,UpdateQuantityVM vm)
        {
            _cartItemService.UpdateQuantity(cartId, productId, vm);
            return RedirectToAction("Index", new { cartId });
        }
        public IActionResult Delete(int cartId,int productId)
        {
            _cartItemService.RemoveItem(cartId, productId);
            return RedirectToAction("Index", new { cartId });
        }

    }
}
