using AutoMapper;
using P_03ProductShop.App.ModelsDto;
using P_03ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P_03ProductShop.App
{
   public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto,User>().ReverseMap();

            CreateMap<ProductDto, Product>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();


        }
    }
}
