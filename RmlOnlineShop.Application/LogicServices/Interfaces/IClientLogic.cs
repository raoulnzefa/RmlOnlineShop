using Microsoft.AspNetCore.Http;
using RmlOnlineShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmlOnlineShop.Application.LogicServices.Interfaces
{
   public interface IClientLogic
    {
        ClientOrderInformatiomViewModel GetClientOrderInfo(ISession session);
        bool SaveClientOrderInfo(ISession session, ClientOrderInformatiomViewModel clientOrderInformatiomViewModel);
        ProductsAndOrderInfoViewModel GetProductsAndOrderInfo(ISession session);
        Task<bool> SaveOrder(OrderInfoViewModel orderInfoViewModel);
        OrderInfoFullViewModel GetOrderByUniqueId(string uniqueId);
    }
}
