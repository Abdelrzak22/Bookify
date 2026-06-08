using AutoMapper;
using Bookify.Web.Core.ViewModel;

namespace Bookify.Web.Core.mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoriesViewModel>();
            CreateMap<FormViewModel, Category>().ReverseMap();


            CreateMap<Author, AuthorViewModel>();
            CreateMap<AuthorFormViemModel, Author>().ReverseMap();
        }
    }
}
