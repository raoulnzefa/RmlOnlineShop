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
        public ClientLogic()
        {

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
    }

}
