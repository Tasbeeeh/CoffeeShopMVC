using CoffeeShopBLL.ModelVMs.Order;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using Microsoft.AspNetCore.Identity;

using CoffeeShopDAL.Entities.Enums;
using CoffeeShopDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopPL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderService orderService, ICartService cartService, IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _cartService = cartService;
            _orderRepository = orderRepository;
            _userManager = userManager;

        }

        // GET: /Order
        public IActionResult Index()
        {
            var orders = _orderService.GetAll();
            //return View(orders);
            //var userId = _userManager.GetUserId(User);
            //var orders = _orderService.GetUserOrders(userId);
            return View(orders); 
        }
        public IActionResult MyOrders()
        {
            //var orders = _orderService.GetAll();
            //return View(orders);
            var userId = _userManager.GetUserId(User);
            var orders = _orderService.GetUserOrders(userId);
            return View(orders);
        }
        public IActionResult Details(int id)
        {
            var orderDetails = _orderService.GetOrderDetails(id);

            if (orderDetails == null)
                return NotFound();

            return View(orderDetails);
        }


        // GET: /Order/Details/1
        //public IActionResult Details(int id)
        //{
        //    var order = _orderService.GetById(id);
        //    if (order == null)
        //        return NotFound();

        //    return View(order);
        //}

        // POST: /Order/PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(string userId)
        {
            try
            {
                var cart = _cartService.GetCartWithItems(userId);

                if (cart == null || !cart.CartItems.Any())
                {
                    TempData["Error"] = "Cart is empty!";
                    return RedirectToAction("Index", "Cart");
                }

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

                return RedirectToAction("Confirmed", "Order", new { orderId = order.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }

        public IActionResult Confirmed(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
                return NotFound();

            return View(order);
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
                return RedirectToAction("MyOrders");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteIfPending(int id)
        {
            try
            {
                bool deleted = _orderService.DeleteIfPending(id);
                if (deleted)
                    TempData["Success"] = "Order deleted successfully.";
                else
                    TempData["Error"] = "Order cannot be deleted because it is Confirmed.";

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