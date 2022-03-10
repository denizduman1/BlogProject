using BlogProject.Data.Abstract;
using BlogProject.Data.Concrete.EntityFramework.Contexts;
using BlogProject.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Concrete.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogProjectContext _context;
       
        public UnitOfWork(BlogProjectContext context)
        {
            _context = context;
        }

        public IArticleRepository ArticleRepository => new EfArticleRepository(_context);

        public ICategoryRepository CategoryRepository => new EfCategoryRepository(_context);

        public IUserRepository UserRepository => new EfUserRepository(_context);

        public IRoleRepository RoleRepository => new EfRoleRepository(_context);

        public ICommentRepository CommentRepository => new EfCommentRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
