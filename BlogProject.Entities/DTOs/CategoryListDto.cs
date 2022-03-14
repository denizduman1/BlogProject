using BlogProject.Entities.Concrete;
using BlogProject.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace BlogProject.Entities.DTOs
{
    public class CategoryListDto : DtoGetBase
    {
        public IList<Category> Categories { get; set; }
    }
}
