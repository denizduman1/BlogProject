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
        private EfArticleRepository _articleRepository;
        private EfCategoryRepository _categoriyRepository;
        private EfCommentRepository _commentRepository;
        private EfRoleRepository _roleRepository;
        private readonly EfUserRepository _userRepository;   
       
        public UnitOfWork(BlogProjectContext context)
        {
            _context = context;
        }

        public IArticleRepository ArticleRepository => _articleRepository ?? new EfArticleRepository(_context);

        public ICategoryRepository CategoryRepository => _categoriyRepository ?? new EfCategoryRepository(_context);

        public IUserRepository UserRepository => _userRepository ?? new EfUserRepository(_context);

        public IRoleRepository RoleRepository => _roleRepository ?? new EfRoleRepository(_context);

        public ICommentRepository CommentRepository => _commentRepository ?? new EfCommentRepository(_context);

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
