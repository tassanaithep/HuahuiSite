using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface IProductCategorieService
    {
        void GetProductCategorieList(ref ProductCategorieViewModel productCategorieViewModel);
        void SaveProductCategorie(ProductCategorieViewModel productCategorieViewModel);
        void UpdateProductCategorie(ProductCategorieViewModel productCategorieViewModel);
        void DeleteProductCategorie(ProductCategorieViewModel productCategorieViewModel);
    }
}
