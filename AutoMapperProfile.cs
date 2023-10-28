using AutoMapper;
using FoodAplication.Dtos;
using FoodAplication.Helper;
using FoodAplication.Models;

namespace FoodAplication
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, Menu>();
        }
    }
}
