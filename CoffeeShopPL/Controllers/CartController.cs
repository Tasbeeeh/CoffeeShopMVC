using CoffeeShopBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopPL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Details(int cartId)
        {
            var cart = _cartService.GetCartById(cartId);

            if (cart == null)
                return NotFound();

            return View(cart);
        }

        // GET: /Cart/UserCart?userId=tot
        public IActionResult UserCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var cart = _cartService.GetUserCart(userId);

            return View("Details", cart);
        }

        // POST: /Cart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var cart = _cartService.CreateCart(userId);

            return RedirectToAction("Details", new { cartId = cart.Id });
        }

        // POST: /Cart/Clear/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Clear(int cartId)
        {
            var success = _cartService.ClearCart(cartId);

            if (!success)
                return NotFound();

            return RedirectToAction("Details", new { cartId });
        }

        // POST: /Cart/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int cartId)
        {
            var success = _cartService.DeleteCart(cartId);

            if (!success)
                return NotFound();

            return RedirectToAction("Index", "Home");
        }
    }
}