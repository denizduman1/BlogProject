using BlogProject.Entities.Concrete;
using BlogProject.Shared.Data.Abstract;

namespace BlogProject.Data.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
    }
}
