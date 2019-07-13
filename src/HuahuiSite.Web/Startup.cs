using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Infrastructure;
using HuahuiSite.Web.Areas.Backend.Services.Class;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
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

            #region Session

            // Add Session
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(60);
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

            #region Services

            // Service of Backend
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IProductService, ProductService>();

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
                    template: "{controller=Main}/{action=Home}/{id?}");

                //routes.MapAreaRoute(
                //    name: "Backend",
                //    areaName: "Backend",
                //    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
