using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using HuahuiSite.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HuahuiSite.Infrastructure.Class.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }

        public IEnumerable<SaleModel> GetSaleList()
        {
            return (from sale in HuahuiDbContext.Sale
                    join user in HuahuiDbContext.User on sale.Id equals user.RoleId
                    select new SaleModel
                    {
                        Id = sale.Id,
                        Firstname = sale.Firstname,
                        Lastname = sale.Lastname,
                        Address = sale.Address,
                        PhoneNumber = sale.PhoneNumber,
                        Email = sale.Email,
                        UserId = user.Id,
                        Username = user.Username,
                        Password = user.Password
                    });
        }
    }
}
