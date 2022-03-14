using BlogProject.Entities.Concrete;
using BlogProject.Shared.Entities.Abstract;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Entities.DTOs
{
    public class CategoryDto : DtoGetBase
    {
        public Category Category { get; set; }
    }
}
