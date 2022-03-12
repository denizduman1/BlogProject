using AutoMapper;
using BlogProject.Data.Abstract;
using BlogProject.Entities.Concrete;
using BlogProject.Entities.DTOs;
using BlogProject.Services.Abstract;
using BlogProject.Shared.Utilities.Results.Abstract;
using BlogProject.Shared.Utilities.Results.ComplexTypes;
using BlogProject.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = 1;
            await _unitOfWork.ArticleRepository.AddAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla eklenmiştir");
        }

        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var result = await _unitOfWork.ArticleRepository.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.ArticleRepository.GetAsync(a => a.Id == articleId);
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await _unitOfWork.ArticleRepository.UpdateAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir makale bulunamadı");
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.ArticleRepository.GetAsync(a => a.Id == articleId, a => a.User, a => a.Category);
            if (article != null)
            {
                return new DataResult<ArticleDto>(new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success 
                },ResultStatus.Success);
            }
            return new DataResult<ArticleDto>(null, ResultStatus.Error, "Böyle bir makale bulunamadı");
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.ArticleRepository.GetAllAsync(null,a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                },ResultStatus.Success);
            }
            return new DataResult<ArticleListDto>(null, ResultStatus.Error, "Makaleler bulunamadı");
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.CategoryRepository.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var articles = await _unitOfWork.ArticleRepository.GetAllAsync(a => a.CategoryId == categoryId && a.IsDeleted == false && a.IsActive
               , a => a.User, a => a.Category);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    }, ResultStatus.Success);
                }
                return new DataResult<ArticleListDto>(null, ResultStatus.Error, "Makaleler bulunamadı");
            }
            return new DataResult<ArticleListDto>(null, ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
        {
            var articles = await _unitOfWork.ArticleRepository.GetAllAsync(a => !a.IsDeleted, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                }, ResultStatus.Success);
            }
            return new DataResult<ArticleListDto>(null, ResultStatus.Error, "Makaleler bulunamadı");
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await _unitOfWork.ArticleRepository.GetAllAsync(a => !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                }, ResultStatus.Success);
            }
            return new DataResult<ArticleListDto>(null, ResultStatus.Error, "Makaleler bulunamadı");
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.ArticleRepository.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.ArticleRepository.GetAsync(a => a.Id == articleId);
                await _unitOfWork.ArticleRepository.DeleteAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir makale bulunamadı");
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName = modifiedByName;
            await _unitOfWork.ArticleRepository.UpdateAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success,$"{article.Title} başlıklı makale başarıyla güncellenmiştir.");
        }
    }
}
