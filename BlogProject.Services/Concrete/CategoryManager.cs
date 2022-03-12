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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
           _unitOfWork = unitOfWork;
           _mapper = mapper;
        }
        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
                var category = _mapper.Map<Category>(categoryAddDto);
                category.CreatedByName = createdByName;
                category.ModifiedByName = createdByName;
                await _unitOfWork.CategoryRepository.AddAsync(
                category
                ).ContinueWith(t => _unitOfWork.SaveAsync()); // kaydetme olmadan front end'e sonucu aktarıyor.
                
                return new Result(ResultStatus.Success,message: $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir.");
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == categoryId);
            if (category is not null)
            {
                category.IsDeleted = true;
            }
            else
            {
                return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
            }
            await _unitOfWork.CategoryRepository.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, message: $"{category.Name} adlı kategori başarıyla silinmiştir.");
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == categoryId,c => c.Articles);
            if (category is not null)
            {
                return new DataResult<CategoryDto>(new CategoryDto { Category = category},ResultStatus.Success);
            }
            return new DataResult<CategoryDto>(null, ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync(predicate:null,c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(new CategoryListDto { Category = categories,ResultStatus = ResultStatus.Success}, ResultStatus.Success);
            }
            return new DataResult<CategoryListDto>(null, ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync(predicate: c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(new CategoryListDto { Category = categories, ResultStatus = ResultStatus.Success }, ResultStatus.Success);
            }
            return new DataResult<CategoryListDto>(null, ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync(predicate: c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(new CategoryListDto { Category = categories, ResultStatus = ResultStatus.Success }, ResultStatus.Success);
            }
            return new DataResult<CategoryListDto>(null, ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == categoryId);
            if (category is not null)
            {
                await _unitOfWork.CategoryRepository.DeleteAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, message: $"{category.Name} adlı kategori veritabanından başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;
            await _unitOfWork.CategoryRepository.UpdateAsync(category).ContinueWith(t=>_unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{category.Name} adlı kadetogori başarıyla güncellenmiştir.");
        }
    }
}
