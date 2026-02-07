using CoffeeShopBLL.ModelVMs.Order;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Entities.Enums;
using CoffeeShopDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopPL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderService orderService, ICartService cartService, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _cartService = cartService;
            _orderRepository = orderRepository;
        }

        // GET: /Order
        public IActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }

        // GET: /Order/Details/1
        public IActionResult Details(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: /Order/PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void PlaceOrder(string userId)
        {
            var cart = _cartService.GetCartWithItems(userId);

            if (cart == null || !cart.CartItems.Any())
                throw new Exception("Cart is Empty!");

            Order order = new Order()
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice),
                OrderStatus = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };

            foreach (var ci in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice
                };
                order.OrderItems.Add(orderItem);
            }

            _orderRepository.AddOrder(order);
            _orderRepository.Save();

            _cartService.ClearCart(cart.Id);
        }


        // POST: /Order/ChangeStatus/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int orderId, string status)
        {
            if (!Enum.TryParse<OrderStatus>(status, out var newStatus))
            {
                TempData["Error"] = "Invalid status";
                return RedirectToAction("Index");
            }

            _orderService.ChangeOrderStatus(orderId, newStatus);
            return RedirectToAction("Index");
        }


        // POST: /Order/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _orderService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}