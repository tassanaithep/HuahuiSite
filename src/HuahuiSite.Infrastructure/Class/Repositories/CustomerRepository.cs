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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }

        public IEnumerable<CustomerModel> GetCustomerList()
        {
            return (from customer in HuahuiDbContext.Customer
                    join user in HuahuiDbContext.User on customer.Id equals user.RoleId
                    select new CustomerModel
                    {
                        Id = customer.Id,
                        Firstname = customer.Firstname,
                        Lastname = customer.Lastname,
                        Address = customer.Address,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email,
                        SaleId = customer.SaleId,
                        UserId = user.Id,
                        Username = user.Username,
                        Password = user.Password
                    });
        }
    }
}
