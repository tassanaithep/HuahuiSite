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
        void GetProductGroupList(ref ProductGroupViewModel productGroupViewModel);
        void SaveProductGroup(ProductGroupViewModel productGroupViewModel);
        void UpdateProductGroup(ProductGroupViewModel productGroupViewModel);
        void DeleteProductGroup(ProductGroupViewModel productGroupViewModel);
    }
}
