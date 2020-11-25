using Microsoft.AspNetCore.Http;
using RmlOnlineShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RmlOnlineShop.Application.LogicServices.Interfaces
{
   public interface IClientLogic
    {
        ClientOrderInformatiomViewModel GetClientOrderInfo(ISession session);
        bool SaveClientOrderInfo(ISession session, ClientOrderInformatiomViewModel clientOrderInformatiomViewModel);
    }
}
