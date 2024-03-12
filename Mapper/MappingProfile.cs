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

            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
                
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Orders, opt => opt.Ignore());

            //CreateMap<List<OrderDTO>, List<Order>>().ReverseMap();

        }
    }
}
