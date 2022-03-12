using AutoMapper;
using BlogProject.Entities.Concrete;
using BlogProject.Entities.DTOs;
using System;

namespace BlogProject.Services.AutoMapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto, Category>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
        }
    }
}
