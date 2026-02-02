using Azure;
using CoffeeShopBLL.Mapping;
using CoffeeShopBLL.ModelVMs.Product;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Classes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Add(ProductVM obj, string imgPath)
        {
            Product product = new Product
            {
                Name = obj.Name,
                Description = obj.Description,
                Image = imgPath,
                ProductSize = obj.ProductSize,
                Price = obj.Price,
                InStock = obj.InStock,
                Quantity = obj.Quantity,
                CategoryId = obj.CategoryId
            };

            _productRepository.Add(product);
        }


        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public void Edit(ProductVM obj)
        {
            Product product = new Product
            {
                Name = obj.Name,
                Description = obj.Description,
                Image = obj.Image,
                ProductSize = obj.ProductSize,
                Price = obj.Price,
                InStock = obj.InStock,
                Quantity = obj.Quantity,
                CategoryId = obj.CategoryId

            };
            _productRepository.Edit(product);
        }

        public List<ProductVM> GetAll()
        {
           return  _productRepository.GetAll().Select(p=>p.ProductMap()).ToList();
        }
        public ProductVM GetById(int id)
        {
            return _productRepository.GetById(id).ProductMap();
        }

        public int Save()
        {
           return _productRepository.Save();
        }

        public ProducsPerPageVM GetProductsPerPage(int page, int pageSize)
        {
             int totalCount = _productRepository.GetAll().Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var products = GetAll().OrderBy(p => p.Id)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(p => new ProductVM
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        Description = p.Description,
                                        Price = p.Price,
                                        Image = p.Image
                                    })
                                    .ToList();
            return new ProducsPerPageVM
            {
                Products = products,
                Page = page,
                TotalPages = totalPages
            };
        }
    }
}
