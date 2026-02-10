using CoffeeShopBLL.ModelVMs.Product;
using CoffeeShopBLL.Services.Classes;
using CoffeeShopBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CoffeeShopPL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICartService _cartService;

        public ProductController(IProductService productService, ICategoryService categoryService,ICartService cartService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cartService   = cartService;
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
        public IActionResult Browse(int page = 1)
        {
            int pageSize = 8;

            var result = _productService.GetProductsPerPage(page, pageSize);
            return View(result);
        }
        public IActionResult Category(string categoryName, int page = 1)
        {
            int pageSize = 8;
            var result = _productService.GetProductsCatPerPage(categoryName, page, pageSize);
            return View(result);
        }
        public IActionResult Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return RedirectToAction("Category");

            var products = _productService.Search(term);

            return View(products);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM,IFormFile img)
        {
            string imgName = null;
            if (img != null)
            {
                imgName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                "images");
                string fullPath = Path.Combine(folder, imgName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
            }
            if (ModelState.IsValid)
            {
                _productService.Add(productVM, imgName);
                _productService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View("Create", productVM);
        }
        public IActionResult Details(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
                return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var cart = _cartService.GetUserCart(userId);
            var vm = new CoffeeShopBLL.ModelVMs.Product.ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Image = product.Image,
                CategoryName = product.CategoryName,
                Price = product.Price,
                Quantity = product.Quantity,
                ProductSize = product.ProductSize,
                CategoryId = cart.Id 
            };


            return View("Details",vm);
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
        public IActionResult Edit(ProductVM productVM, IFormFile img)
        {
            string imgName = null!;
            if (img != null)
            {
                imgName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                                "assets",
                                "img");
                string fullPath = Path.Combine(folder, imgName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
            }
            if (ModelState.IsValid)
            {
                _productService.Edit(productVM, imgName);
                _productService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View("Edit",productVM);
        }
    }
}
