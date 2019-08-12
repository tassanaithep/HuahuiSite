using HuahuiSite.Web.Areas.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Services.Interface
{
    public interface IPromotionService
    {
        void GetPromotionList(ref MainViewModel shopListViewModel);
    }
}
