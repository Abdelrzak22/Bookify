using AutoMapper;
using Bookify.Web.Core.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookify.Web.Core.mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //categories
            CreateMap<Category, CategoriesViewModel>();
            CreateMap<FormViewModel, Category>().ReverseMap();
            CreateMap<Category,SelectListItem>()
                .ForMember(dest=>dest.Value,opt=> opt.MapFrom(src=>src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            //authors
            CreateMap<Author, AuthorViewModel>();
            CreateMap<AuthorFormViemModel, Author>().ReverseMap();

            CreateMap<Author, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            //Books
            CreateMap<BookFormViewModel, Book>()

                .ReverseMap()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());



        }
    }
}
