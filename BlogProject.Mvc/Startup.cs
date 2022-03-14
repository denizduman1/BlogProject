using BlogProject.Services.AutoMapper.Profiles;
using BlogProject.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Mvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); //�al��ma zaman� de�i�ikleri g�rebilmek i�in
            services.AddAutoMapper(typeof(CategoryProfile),typeof(ArticleProfile)); // derlenme esnas�nda auto mapper'in bu s�n�flar� taramas�n� sa�l�yor.
            services.LoadMyServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); // olmayan sayfalara gitti�imizde 404 hatas� verecektir.
            }

            app.UseStaticFiles(); // statik dosyalar� kullanmak i�in

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(  //admin alan� i�in
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute(); // varsay�lan Home/Index
            });
        }
    }
}
