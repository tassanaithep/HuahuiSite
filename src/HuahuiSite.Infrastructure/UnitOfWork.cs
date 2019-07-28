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
            Carts = new CartRepository(_context);
            CartItemLists = new CartItemListRepository(_context);
            Customers = new CustomerRepository(_context);
            Products = new ProductRepository(_context);
            ProductCategories = new ProductCategorieRepository(_context);
            ProductGroups = new ProductGroupRepository(_context);
            Sales = new SaleRepository(_context);
            UnitOfProducts = new UnitOfProductRepository(_context);
            Users = new UserRepository(_context);
        }

        public ICartRepository Carts { get; private set; }
        public ICartItemListRepository CartItemLists { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IProductRepository Products { get; private set; }
        public IProductCategorieRepository ProductCategories { get; private set; }
        public IProductGroupRepository ProductGroups { get; private set; }
        public ISaleRepository Sales { get; private set; }
        public IUnitOfProductRepository UnitOfProducts { get; private set; }
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
