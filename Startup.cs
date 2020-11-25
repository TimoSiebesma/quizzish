using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Newtonsoft.Json.Serialization;
using Quizzish.Data.DbContexts;
using Quizzish.Data.Repositories.CacheData;
using Quizzish.Data.Session;
using Quizzish.Data.UnitOfWork;
using Quizzish.MailServices;
using Quizzish.Models;
using Quizzish.Profiles;

namespace Quizzish
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddControllers()
               .AddNewtonsoftJson(s =>
                   s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()
               );

            services.AddDbContext<IdentityContext>(
                opt => opt.UseSqlServer(_config.GetConnectionString("Identity"))
            );

            services.AddDbContext<QuizzishDbContext>(
                opt => 
                {
                    opt.UseLazyLoadingProxies();
                    opt.UseSqlServer(_config.GetConnectionString("Quizzish"));
                    opt.ConfigureWarnings(w => w.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
                }
            );

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddScoped<Session>();
            services.AddScoped<CustomMapper>();
            services.AddScoped<Cache<IHaveId>>();
            services.AddScoped<MailService>();

            services.AddMemoryCache();

            services.AddIdentity<IdentityUser, IdentityRole>(
                config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMailKit(config => config
              .UseMailKit(_config.GetSection("Email").Get<MailKitOptions>())
            );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
