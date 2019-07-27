//using AutoMapper;
//using EcommerceCore.Domain.Entities;
//using EcommerceCore.Services.Infrastructure.ViewModels;

//namespace EcommerceCore.Services.Infrastructure.Mapping
//{
//    public class MappingProfile : Profile
//    {
//        public MappingProfile()
//        {
//            CreateMapFromEntitiesToViewModel();
//            CreateMapFromViewModelToEntities();
//        }

//        private void CreateMapFromViewModelToEntities()
//        {
//            CreateMap<SupplierViewModel, Supplier>()
//                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
//                .ForMember(dest => dest.UpdatedDate, src => src.Ignore());
//            CreateMap<ProductViewModel, Product>()
//                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
//                .ForMember(dest => dest.UpdatedDate, src => src.Ignore());
//            CreateMap<ManufacturerViewModel, Manufacturer>()
//                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
//                .ForMember(dest => dest.UpdatedDate, src => src.Ignore());
//            CreateMap<CategoryViewModel, Category>()
//                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
//                .ForMember(dest => dest.UpdatedDate, src => src.Ignore());
//            CreateMap<CouponViewModel, Coupon>()
//                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
//                .ForMember(dest => dest.UpdatedDate, src => src.Ignore());
//            CreateMap<CatalogCouponViewModel, CatalogCoupon>()
//                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
//                .ForMember(dest => dest.UpdatedDate, src => src.Ignore());
//        }

//        private void CreateMapFromEntitiesToViewModel()
//        {
//            CreateMap<Supplier, SupplierViewModel>();
//            CreateMap<Product, ProductViewModel>();
//            CreateMap<Manufacturer, ManufacturerViewModel>();
//            CreateMap<Category, CategoryViewModel>();
//            CreateMap<Coupon, CouponViewModel>();
//            CreateMap<CatalogCoupon, CatalogCouponViewModel>();
//        }
//    }
//}
