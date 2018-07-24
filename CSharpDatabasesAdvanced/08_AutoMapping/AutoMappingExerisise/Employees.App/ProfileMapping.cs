using AutoMapper;
using Employees.App.ModelsDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App
{
  public  class ProfileMapping:Profile
    {
        public ProfileMapping()
        {
            CreateMap<Employees.Models.Employee, EmployeeDto>()
                .ForMember(e=>e.ManagerLastName,opt=>opt.MapFrom(x=>x.Manager.Lastname)).ReverseMap();

            CreateMap<Employees.Models.Employee, ManagerDto>()
                .ForMember(dest=>dest.EmployeesDto,from=>from.MapFrom(e=>e.Employees))
                .ReverseMap();
        }
    }
}
