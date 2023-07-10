using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperShop_Mariana.Data;
using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Azure;
using Azure.Storage.Queues;
using Azure.Storage.Blobs;
using Azure.Core.Extensions;

namespace SuperShop_Mariana
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
            services.AddIdentity<User, IdentityRole>( cfg =>
            {
                cfg.User.RequireUniqueEmail = true; //Emails unicos

                //Password sem caracteres especiais e etc
                cfg.Password.RequireDigit = false; 
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequiredLength = 6;
                cfg.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<DataContext>();
            

            services.AddDbContext<DataContext>(cfg =>  
            {
                //Tipo de bases dados queremos instalar
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")); //Buscar a connection string
            }
            );
            services.AddTransient<SeedDB>(); //configurar a injeção de dependencias. Usa deita fora e não é mais usado
            //AddSingleton: Nunca vai ser destruido. ocupa muita memória
            //AddScope: Fica criado e instanciado. Quando criamos novo, ele apaga e sobrepõe. 

            services.AddScoped<IUserHelper, UserHelper>();

            services.AddScoped<IBlobHelper, BlobHelper>(); //Entra o blob e sai o Iimage.

            services.AddScoped<IConverterHelper, ConverterHelper>();
            
            services.AddScoped<IProductsRepository, ProductRepository>(); //Class por herança. Class Abstrata
            //services.AddScoped<IRepository, Repository>(); 

            services.AddScoped<IOrderRepository, OrderRepository>();


            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/NotAuthorized";
                options.AccessDeniedPath = "/Account/NotAuthorized";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
