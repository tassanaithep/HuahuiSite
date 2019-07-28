using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface ISaleService
    {
        void GetSaleList(ref SaleViewModel saleViewModel);
        void GetSaleSelectList(ref CustomerViewModel customerViewModel);
        int SaveSale(SaleViewModel saleViewModel);
        void UpdateSale(SaleViewModel saleViewModel);
        void DeleteSale(SaleViewModel saleViewModel);
    }
}
