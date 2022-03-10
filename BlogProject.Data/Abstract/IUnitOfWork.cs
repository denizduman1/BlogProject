using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IArticleRepository ArticleRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        ICommentRepository CommentRepository { get; }
        Task<int> SaveAsync();
    }
}
