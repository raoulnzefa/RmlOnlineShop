using Microsoft.Extensions.Logging;
using RmlOnlineShop.Application.ViewModels;
using RmlOnlineShop.Database.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmlOnlineShop.Application.DataServices.Interfaces
{
    public interface IStockManager
    {
        IEnumerable<StockViewModel> GetAllStocksAsViewModel();
        Task<StockViewModel> UpdateStockByViewModel(StockViewModel stockViewModel);

        Task<bool> DeleteStock(int id);
        Task<StockViewModel> CreateStockByViewModel(CreateStockViewModel createStockViewModel);
        IEnumerable<StockViewModel> GetAllStocksAsViewModelByProductId(int productId);
        IEnumerable<ProductWithStocksViewModel> GetAllProductsWithStocks();

    }
}
