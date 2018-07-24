using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Forum.App.Models;
using Forum.Data.Models;

namespace Forum.App
{
  public  class ForumProfile:Profile
    {
        public ForumProfile()
        {
          
               CreateMap<Post, PostDetailsDto>()
                    .ForMember(dto => dto.ReplyCount, dest => dest.MapFrom(post => post.Replies.Count));
               CreateMap<Reply,ReplyDto>();
               CreateMap<User, UserDto>();
               CreateMap<Category, CategoryDto>();

        }
    }
}
