using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Dtos;
using TeamBuilder.Models;

namespace TeamBuilder.App
{
  public  class ProfileMapping:Profile
    {
        public ProfileMapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
