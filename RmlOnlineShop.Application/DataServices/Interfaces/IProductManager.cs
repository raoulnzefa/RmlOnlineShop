using RmlOnlineShop.Application.ViewModels;
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
        IEnumerable<AllProductsViewModel> GetAllProductsAsViewModel();
        Product GetProductById(int id);
        Task<bool> DeleteProduct(int id);
        Task<Product> UpdateProductByViewModel(ProductViewModel productViewModel);
        Task<Product> UpdateProductByProperties(int id, string name, string description, decimal price);
        Task<Product> CreateProductByViewModel(ProductViewModel productViewModel);
        ProductWithStocksViewModel GetProductViewModelById(int id);

    }
}
