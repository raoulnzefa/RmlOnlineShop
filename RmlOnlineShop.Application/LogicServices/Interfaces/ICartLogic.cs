using Microsoft.AspNetCore.Http;
using RmlOnlineShop.Application.SessionModels;
using RmlOnlineShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmlOnlineShop.Application.LogicServices.Interfaces
{
    public interface ICartLogic
    {
        IEnumerable<ProductsInCartViewModel> GetProductInCartAsViewModel(ISession session);
        Task<bool> AddToCart(ISession session, ProductCart productCart);
        List<ProductCart> GetCart(ISession session);
    }
}
