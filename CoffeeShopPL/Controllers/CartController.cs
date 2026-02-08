using CoffeeShopBLL.ModelVMs.Cart;
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

        public IActionResult Index()
        {
            var carts = _cartService.GetAll();
            return View(carts);
        }

        public IActionResult Details(int id = 4)
        {
            var cart = _cartService.GetById(id);
            if (cart == null || !cart.Items.Any())
                return View("EmptyCart"); 

            return View(cart);
        }

        [HttpPost]
        public IActionResult Add(CartViewModel cart)
        {
            if (ModelState.IsValid)
            {
                _cartService.Add(cart);
                _cartService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        [HttpPost]
        public IActionResult Update(CartViewModel cart)
        {
            if (ModelState.IsValid)
            {
                _cartService.Edit(cart);
                _cartService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        public IActionResult Delete(int id)
        {
            _cartService.Delete(id);
            _cartService.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            var carts = _cartService.GetAll();
            foreach (var cart in carts)
            {
                _cartService.Delete(cart.Id);
            }
            _cartService.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
