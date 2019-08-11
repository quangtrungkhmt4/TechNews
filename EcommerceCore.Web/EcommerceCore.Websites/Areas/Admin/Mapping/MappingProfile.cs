using AutoMapper;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Websites.Areas.Admin.Models.ViewModels;

namespace EcommerceCore.Websites.Areas.Admin.Mapping
{
    public class MappingAdminProfile : Profile
    {
        public MappingAdminProfile()
        {
            CreateMapFromEntitiesToViewModel();
            CreateMapFromViewModelToEntities();
        }

        private void CreateMapFromViewModelToEntities()
        {            
            CreateMap<CategoryViewModel, Category>()
                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedDate, src => src.Ignore());
           
        }

        private void CreateMapFromEntitiesToViewModel()
        {
            CreateMap<Category, CategoryViewModel>();           
        }
    }
}
