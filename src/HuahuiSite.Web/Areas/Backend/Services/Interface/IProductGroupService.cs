using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface IProductGroupService
    {
        void GetProductGroupList(ref ProductGroupViewModel productGroupViewModel, string keywordForSearch, bool isUpdate, int page);
        void SaveProductGroup(ProductGroupViewModel productGroupViewModel);
        void UpdateProductGroup(ProductGroupViewModel productGroupViewModel);
        void DeleteProductGroup(ProductGroupViewModel productGroupViewModel);
    }
}
