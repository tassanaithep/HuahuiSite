using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface IProductService
    {
        void GetProductList(ref ProductViewModel productViewModel, string keywordForSearch);
        void SaveProduct(ProductViewModel productViewModel);
        void UpdateProduct(ProductViewModel productViewModel);
        void DeleteProduct(ProductViewModel productViewModel);
    }
}
