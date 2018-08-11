//using AutoMapper;
//using Instagraph.DataProcessor.Dtos;
//using Instagraph.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Instagraph.DataProcessor
//{
//   public class MappingProfile:Profile
//    {
//        public MappingProfile()
//        {
//            CreateMap<User, UserByTopPostsDto>()
//                .ForMember
//                (
//                dto => dto.Username,
//                src => src.MapFrom(u => u.Username)
//                )
//                .ForMember
//                (
//                dto => dto.MostComments,
//                src => src.MapFrom(u => u.Posts.Count > 0 ? u.Posts.Max(p => p.Comments.Count) : 0)
//                );
//        }
//    }
//}
