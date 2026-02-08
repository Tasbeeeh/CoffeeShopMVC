using CoffeeShopBLL.ModelVMs.Order;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopPL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            //var orders = _orderService.GetAll();
            //return View(orders);
            var userId = _userManager.GetUserId(User);
            var orders = _orderService.GetUserOrders(userId);
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                _orderService.Add(orderVM);
                _orderService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(orderVM);
        }

        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            _orderService.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var orderVM = _orderService.GetById(id);
            if (orderVM == null)
            {
                return NotFound();
            }
            return View(orderVM);
        }

        [HttpPost]
        public IActionResult Edit(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                _orderService.Edit(orderVM);
                _orderService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(orderVM);
        }
    }
}