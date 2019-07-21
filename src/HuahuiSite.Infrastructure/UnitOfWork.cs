using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Interfaces.Repositories;
using HuahuiSite.Infrastructure.Class.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HuahuiDbContext _context;

        public UnitOfWork(HuahuiDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            Products = new ProductRepository(_context);
            Sales = new SaleRepository(_context);
            Users = new UserRepository(_context);
        }

        public ICustomerRepository Customers { get; private set; }
        public IProductRepository Products { get; private set; }
        public ISaleRepository Sales { get; private set; }
        public IUserRepository Users { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
