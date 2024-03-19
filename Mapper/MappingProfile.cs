using AutoMapper;
using ShoppingCartAPIs.DTOs;
using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<ProductDTO, Product>()
                 .ForMember(dest => dest.Category, opt => opt.Ignore()).ReverseMap();

            CreateMap<UserReviewDTO, UserReview>()
                .ForMember(dest => dest.Product, opt => opt.Ignore()).ReverseMap();

            CreateMap<OrderDTO, Order>();

            CreateMap<PlacedOrderDTO, PlacedOrder>()
                //.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                //.ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore()).ReverseMap();


            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Orders, opt => opt.Ignore());



        }
    }
}
