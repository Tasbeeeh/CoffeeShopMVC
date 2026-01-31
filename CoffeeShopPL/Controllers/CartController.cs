using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShopPL.Controllers
{
    [Authorize] // Only logged-in users can access the cart
    public class CartController : Controller
    {
        public class CartProduct
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Total => Price * Quantity;
        }

        private const string CartSessionKey = "ShoppingCart";

        // Helper: Get current cart from session (WITH SERIALIZATION)
        private List<CartProduct> GetCart()
        {
            // Try to get the cart JSON string from session
            var cartJson = HttpContext.Session.GetString(CartSessionKey);

            // If it's null or empty, return new empty cart
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartProduct>();
            }

            // Convert JSON string back to List<CartProduct>
            return JsonSerializer.Deserialize<List<CartProduct>>(cartJson);
        }

        // Helper: Save cart to session (WITH SERIALIZATION)
        private void SaveCart(List<CartProduct> cart)
        {
            // Convert cart to JSON string
            var cartJson = JsonSerializer.Serialize(cart);
            // Store JSON string in session
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }

        // GET: /Cart
        public IActionResult Index()
        {
            var cart = GetCart();
            ViewBag.CartTotal = cart.Sum(item => item.Total);
            return View(cart);
        }

        // POST: /Cart/Add
        [HttpPost]
        public IActionResult Add(int productId, string productName, decimal price, int quantity = 1)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(p => p.Id == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartProduct
                {
                    Id = productId,
                    Name = productName,
                    Price = price,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // GET: /Cart/Remove/{id}
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.Id == id);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        // POST: /Cart/Checkout
        [HttpPost]
        public IActionResult Checkout()
        {
            HttpContext.Session.Remove(CartSessionKey);
            TempData["Message"] = "Order placed successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}