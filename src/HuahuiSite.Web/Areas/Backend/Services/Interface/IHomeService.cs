using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface IHomeService
    {
        void GetCompleteOrderList(ref HomeViewModel homeViewModel);
        void Search(ref HomeViewModel homeViewModel);
        void GetProductPriceByQuantity(ref ProductGroupViewModel productGroup, string productGroupCode, int quantity);
        void ExportToFile(ref byte[] result, string orderId);
    }
}
