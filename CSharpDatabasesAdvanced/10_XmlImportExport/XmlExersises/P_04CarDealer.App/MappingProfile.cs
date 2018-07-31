using AutoMapper;
using P_04CarDealer.App.ModelsDto;
using P_04CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P_04CarDealer.App
{
   public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<SupplierDto, Supplier>().ReverseMap();
            CreateMap<PartDto, Part>().ReverseMap();
            CreateMap<CarDto, Car>().ReverseMap();
            CreateMap<CustomerDto, Customer>().ReverseMap();
        }
    }
}
