using BlogProject.Data.Abstract;
using BlogProject.Data.Concrete.EntityFramework;
using BlogProject.Data.Concrete.EntityFramework.Contexts;
using BlogProject.Services.Abstract;
using BlogProject.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //asp core dependencyInjection
        public static IServiceCollection LoadMyServices(this IServiceCollection servicCollection)
        {
            servicCollection.AddDbContext<BlogProjectContext>();
            servicCollection.AddScoped<IUnitOfWork, UnitOfWork>(); // işlemler scope'da tutulur.
            servicCollection.AddScoped<ICategoryService, CategoryManager>();
            servicCollection.AddScoped<IArticleService,ArticleManager>();
            return servicCollection;
        }
    }
}
