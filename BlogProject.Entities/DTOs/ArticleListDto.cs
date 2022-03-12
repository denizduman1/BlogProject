using BlogProject.Entities.Concrete;
using BlogProject.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace BlogProject.Entities.DTOs
{
    public class ArticleListDto : DtoGetBase
    {
        public IList<Article> Articles { get; set; }
    }
}
