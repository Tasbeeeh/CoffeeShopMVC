using CoffeeShopBLL.ModelVMs.Category;
using CoffeeShopBLL.Services.Classes;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopPL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(
            ICategoryService categoryService,
            IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public IActionResult Browse(int? categoryId)
        {
            var categories = _categoryService.GetAll();

            //ViewBag.SelectedCategoryId = categoryId;

            //if (categoryId != null)
            //{
            //    ViewBag.Products =
            //        _productService.GetByCategoryId(categoryId.Value);
            //}

            return View(categories);
        }

        // ================= ADMIN PAGES =================
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Add(categoryVM);
                _categoryService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);
        }
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            _categoryService.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var categoryVM = _categoryService.GetById(id);
            if (categoryVM == null)
            {
                return NotFound();
            }
            return View(categoryVM);
        }
        [HttpPost]
        public IActionResult Edit(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Edit(categoryVM);
                _categoryService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);

        }
    }
}
