using CoffeeShopBLL.ModelVMs.Product;
using CoffeeShopBLL.Services.Classes;
using CoffeeShopBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoffeeShopPL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
           var products= _productService.GetAll();
            return View("Index",products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(productVM);
                _productService.Save();
                return RedirectToAction("Index");
            }
            return View("Create", productVM);
        }

        public IActionResult Delete(int id) 
        {
            _productService.Delete(id);
            _productService.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View("Edit", product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _productService.Edit(productVM);
                _productService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View("Edit",productVM);
        }
    }
}
