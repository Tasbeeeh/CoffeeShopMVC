using CoffeeShopBLL.Services.Interfaces;
﻿using CoffeeShopBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public IActionResult GoToCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account"); 

            var cartId = _cartService.GetLatestCartId(userId);

            return Redirect($"/Cart/Details?cartId={cartId}");
        }












        public IActionResult UserCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var cart = _cartService.GetUserCart(userId);

            return View("Details", cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var cart = _cartService.CreateCart(userId);

            return RedirectToAction("Details", new { cartId = cart.Id });
        }

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