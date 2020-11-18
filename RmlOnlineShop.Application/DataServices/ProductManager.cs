using Microsoft.Extensions.Logging;
using RmlOnlineShop.Application.DataServices.Interfaces;
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

    }
}
