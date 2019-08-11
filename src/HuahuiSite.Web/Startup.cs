using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Infrastructure;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Class;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using HuahuiSite.Web.Areas.Frontend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HuahuiSite.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Initialize Session

            // Add Session
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            #endregion

            services.AddHttpContextAccessor();

            // Initialize DbContext
            services.AddDbContext<HuahuiDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HuahuiDatabase")));

            // Initialize UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region Initialize Services

            #region Backend

            services.AddScoped<HuahuiSite.Web.Areas.Backend.Services.Interface.ILoginService, HuahuiSite.Web.Areas.Backend.Services.Class.LoginService>();
            services.AddScoped<HuahuiSite.Web.Areas.Backend.Services.Interface.ICartService, HuahuiSite.Web.Areas.Backend.Services.Class.CartService>();
            services.AddScoped<HuahuiSite.Web.Areas.Backend.Services.Interface.IOrderService, HuahuiSite.Web.Areas.Backend.Services.Class.OrderService>();
            services.AddScoped<HuahuiSite.Web.Areas.Backend.Services.Interface.ICustomerService, HuahuiSite.Web.Areas.Backend.Services.Class.CustomerService>();
            services.AddScoped<HuahuiSite.Web.Areas.Backend.Services.Interface.IProductService, HuahuiSite.Web.Areas.Backend.Services.Class.ProductService>();
            services.AddScoped<HuahuiSite.Web.Areas.Backend.Services.Interface.IProductCategorieService, HuahuiSite.Web.Areas.Backend.Services.Class.ProductCategorieService>();
            services.AddScoped<HuahuiSite.Web.Areas.Backend.Services.Interface.IProductGroupService, HuahuiSite.Web.Areas.Backend.Services.Class.ProductGroupService>();
            services.AddScoped<HuahuiSite.Web.Areas.Backend.Services.Interface.ISaleService, HuahuiSite.Web.Areas.Backend.Services.Class.SaleService>();
            services.AddScoped<HuahuiSite.Web.Areas.Backend.Services.Interface.IUserService, HuahuiSite.Web.Areas.Backend.Services.Class.UserService>();

            #endregion

            #region Frontend

            services.AddScoped<HuahuiSite.Web.Areas.Frontend.Services.Interface.ILoginService, HuahuiSite.Web.Areas.Frontend.Services.Class.LoginService>();
            services.AddScoped<HuahuiSite.Web.Areas.Frontend.Services.Interface.IHomeService, HuahuiSite.Web.Areas.Frontend.Services.Class.HomeService>();
            services.AddScoped<HuahuiSite.Web.Areas.Frontend.Services.Interface.ICartService, HuahuiSite.Web.Areas.Frontend.Services.Class.CartService>();
            services.AddScoped<HuahuiSite.Web.Areas.Frontend.Services.Interface.IShopListService, HuahuiSite.Web.Areas.Frontend.Services.Class.ShopListService>();
            services.AddScoped<HuahuiSite.Web.Areas.Frontend.Services.Interface.IPromotionService, HuahuiSite.Web.Areas.Frontend.Services.Class.PromotionService>();

            #endregion

            #endregion

            #region Initialize Auto Mapper

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SaleModel, SaleViewModel>();
                cfg.CreateMap<CustomerModel, CustomerViewModel>();
                cfg.CreateMap<CartItemListModel, Areas.Frontend.Models.CartItemListViewModel>();
                cfg.CreateMap<ProductModel, Areas.Frontend.Models.ProductViewModel>();

            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Main}/{action=Home}/{id?}");

                routes.MapAreaRoute(
                    name: "Frontend",
                    areaName: "Frontend",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: "Backend",
                    areaName: "Backend",
                    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
