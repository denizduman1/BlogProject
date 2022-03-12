using BlogProject.Entities.Concrete;
using BlogProject.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Entities.DTOs
{
    public class CategoryDto : DtoGetBase
    {
        public Category Category { get; set; }
    }
    public class CategoryListDto : DtoGetBase
    {
        public IList<Category> Category { get; set; }
    }
}
