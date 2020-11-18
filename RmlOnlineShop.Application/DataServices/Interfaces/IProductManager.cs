using RmlOnlineShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmlOnlineShop.Application.DataServices.Interfaces
{
    public interface IProductManager
    {
        Task<Product> CreateProductByProperties(string name, string description = "Product description", decimal price = 0M);
        Task<Product> CreateProduct(Product product);
        List<Product> GetAllProducts();
    }
}
