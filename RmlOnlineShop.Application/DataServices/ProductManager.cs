using Microsoft.Extensions.Logging;
using RmlOnlineShop.Application.DataServices.Interfaces;
using RmlOnlineShop.Application.ViewModels;
using RmlOnlineShop.Data.Models;
using RmlOnlineShop.Database.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmlOnlineShop.Application.DataServices
{
   public class ProductManager : IProductManager
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ILogger<ProductManager> productLogger;


        public ProductManager
            (
                   ApplicationDbContext applicationDbContext,
                   ILogger<ProductManager> logger
            )
        {
            if (applicationDbContext==null)
            {
                throw new NullReferenceException($"{nameof(applicationDbContext)} has been null! Problem with DI?");
            }
            if (logger == null)
            {
                throw new NullReferenceException($"{nameof(logger)} has been null! Problem with DI?");
            }

            this.applicationDbContext = applicationDbContext;
            this.productLogger = logger;
        }


        public async Task<Product> UpdateProductByProperties(int id, string name, string description, decimal price)
        {
            var product = applicationDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product==null)
            {
                return null;
            }

            product.Name = name;
            product.Description = description;
            product.Price = price;

            applicationDbContext.Products.Update(product);
            await applicationDbContext.SaveChangesAsync();
            productLogger.LogInformation($"{product.Name} has been updated!");
            return product;
        }

        public async Task<Product> UpdateProductByViewModel(ProductViewModel productViewModel)
        {
            var product = applicationDbContext.Products.FirstOrDefault(x => x.Id == productViewModel.Id);
            if (product == null)
            {
                return null;
            }

            product.Name = productViewModel.Name;
            product.Description = productViewModel.Description;
            product.Price = productViewModel.Price;

            applicationDbContext.Products.Update(product);
            await applicationDbContext.SaveChangesAsync();
            productLogger.LogInformation($"{product.Name} has been updated!");
            return product;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var product = applicationDbContext.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return false;
            }

            applicationDbContext.Products.Remove(product);
            await applicationDbContext.SaveChangesAsync();
            productLogger.LogInformation($"Deleted product: {product.Name}");
            return true;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            if (product == null)
            {
                return null;
            }

            productLogger.LogInformation($"Created new product: {product.Name}");
            applicationDbContext.Products.Add(product);
            await applicationDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> CreateProductByViewModel(ProductViewModel productViewModel)
        {
            if (productViewModel == null)
            {
                return null;
            }

            var product = new Product {
                Name= productViewModel.Name,
                Description=productViewModel.Description,
                Price=productViewModel.Price
            };

       
            applicationDbContext.Products.Add(product);
            await applicationDbContext.SaveChangesAsync();
            productLogger.LogInformation($"Created new product: {product.Name}");
            return product;
        }

        public async Task<Product> CreateProductByProperties(string name, string description="Product description", decimal price=0M)
        {
            if (name is null)
            {
                return null;
            }

            var product = new Product 
            { 
                Name=name,
                Description=description,
                Price=price
            };

            productLogger.LogInformation($"Created new product: {product.Name}");
            applicationDbContext.Products.Add(product);
            await applicationDbContext.SaveChangesAsync();
            return product;
        }

        public List<Product> GetAllProducts() 
        {
            return applicationDbContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return applicationDbContext.Products.FirstOrDefault(x=>x.Id==id);
        }


        public IEnumerable<AllProductsViewModel> GetAllProductsAsViewModel()
        {
            var products = applicationDbContext.Products.AsEnumerable().Select(x => new AllProductsViewModel 
            {
                Id = x.Id,
                Name=x.Name,
                Description=x.Description,
                Price=x.Price
            });

            return products;
        }

    }
}
