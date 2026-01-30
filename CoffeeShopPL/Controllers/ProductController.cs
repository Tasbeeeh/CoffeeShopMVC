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

        public IActionResult Index(string? searchTerm, string? categoryName, int pageNumber = 1, int pageSize = 10)
        {
            var products = _productService.GetAll();
            var categories = _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Name", "Name", categoryName);

            if (!string.IsNullOrEmpty(categoryName))
            {
                products = _productService.GetProductsByCategoryName(categoryName);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = _productService.Search(searchTerm);
            }

            var totalItems = products.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Pagination
            var pagedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = totalPages;

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryName = categoryName;

            return View("Index", pagedProducts);
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
