using Microsoft.EntityFrameworkCore;
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
    public class StockManager : IStockManager
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ILogger<ProductManager> stockLogger;


        public StockManager(
             ApplicationDbContext applicationDbContext,
            ILogger<ProductManager> stockLogger
            
            )
        {
            this.applicationDbContext = applicationDbContext;
            this.stockLogger = stockLogger;
        }

        public async Task<StockViewModel> CreateStockByViewModel(CreateStockViewModel createStockViewModel)
        {
            if (createStockViewModel == null)
            {
                return null;
            }

            if (applicationDbContext.Products.FirstOrDefault(x=>x.Id==createStockViewModel.ProductId)==null)
            {
                return null;
            }

            Stock stock = new Stock
            {
                Description= createStockViewModel.Description,
                ProductId= createStockViewModel.ProductId,
                Quantity= createStockViewModel.Quantity
            };

            applicationDbContext.Stocks.Add(stock);
            await applicationDbContext.SaveChangesAsync();
            stockLogger.LogInformation($"Created new stock: {stock.Description} with quantity: {stock.Quantity}");
            return new StockViewModel { 
                Id=stock.Id,
                Description=stock.Description,
                ProductId=stock.ProductId,
                Quantity=stock.Quantity
            };
        }

        public async Task<bool> DeleteStock(int id)
        {
            var stock = applicationDbContext.Stocks.FirstOrDefault(x => x.Id == id);

            if (stock == null)
            {
                return false;
            }

            applicationDbContext.Stocks.Remove(stock);
            await applicationDbContext.SaveChangesAsync();
            stockLogger.LogInformation($"Deleted stock: {stock.Description}");
            return true;
        }

        public async Task<StockViewModel> UpdateStockByViewModel(StockViewModel stockViewModel)
        {
            var stock = applicationDbContext.Stocks.FirstOrDefault(x => x.Id == stockViewModel.Id);
            if (stock == null)
            {
                return null;
            }

            stock.Description = stockViewModel.Description;
            stock.Quantity = stockViewModel.Quantity;
            

            applicationDbContext.Stocks.Update(stock);
            await applicationDbContext.SaveChangesAsync();
            stockLogger.LogInformation($"{stock.Description} has been updated!");
            return stockViewModel;
        }

        public IEnumerable<StockViewModel> GetAllStocksAsViewModel()
        {
            return applicationDbContext.Stocks.AsEnumerable().Select(x=> new StockViewModel { 
                Id=x.Id,
                Description=x.Description,
                ProductId=x.ProductId,
                Quantity=x.Quantity
            });
        }

        public IEnumerable<StockViewModel> GetAllStocksAsViewModelByProductId(int productId)
        {
            return applicationDbContext.Stocks
                .Where(x=>x.ProductId==productId)
                .Select(x => new StockViewModel
            {
                Id = x.Id,
                Description = x.Description,
                ProductId = x.ProductId,
                Quantity = x.Quantity
            })
                .AsEnumerable();
        }

        public IEnumerable<ProductWithStocksViewModel> GetAllProductsWithStocks()
        {
            return applicationDbContext.Products
                .Include(x => x.Stock)
                .Select(x => new ProductWithStocksViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price,
                    StocksViewModel = x.Stock.Select(xstock=> new StockViewModel
                    {
                        Description=xstock.Description,
                        Id=xstock.Id,
                        ProductId=xstock.ProductId,
                        Quantity=xstock.Quantity
                    })
                })
                .AsEnumerable()
                ;
        }
    }
}
