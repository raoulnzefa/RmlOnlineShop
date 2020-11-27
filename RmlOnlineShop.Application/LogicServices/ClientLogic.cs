using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RmlOnlineShop.Application.LogicServices.Interfaces;
using RmlOnlineShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RmlOnlineShop.Application.LogicServices
{
    public class ClientLogic : IClientLogic
    {
        private readonly ICartLogic cartLogic;


        public ClientLogic(
            ICartLogic cartLogic
            )
        {
            this.cartLogic = cartLogic;
        }


        public ClientOrderInformatiomViewModel GetClientOrderInfo(ISession session)
        {
            var s = session.GetString("ClientOrderInfo");
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ClientOrderInformatiomViewModel>(s);

        }

        public bool SaveClientOrderInfo(ISession session,ClientOrderInformatiomViewModel clientOrderInformatiomViewModel)
        {
            if (clientOrderInformatiomViewModel == null)
            {
                return false;
            }

            var clientInfo = JsonConvert.SerializeObject(clientOrderInformatiomViewModel);

            session.SetString("ClientOrderInfo", clientInfo);
            return true;
        }

        public ProductsAndOrderInfoViewModel GetProductsAndOrderInfo(ISession session)
        {
            var productsInCart = cartLogic.GetProductInCartAsViewModel(session);
            if (productsInCart==null)
            {
                return null;
            }

            var clientOrderInfo = GetClientOrderInfo(session);
           

            var ProductsAndClientOrderInfo = new ProductsAndOrderInfoViewModel
            {
                ClientOrderInformatiomViewModel=clientOrderInfo,
                ProductsInCartViewModel=productsInCart
            };

            return ProductsAndClientOrderInfo;
        }
    }

}
