using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Instagraph.Data;
using Instagraph.DataProcessor.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
       // private static IMapper mapper = new MapperConfiguration(cfg=>cfg.AddProfile<MappingProfile>()).CreateMapper();

        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var posts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .Select(p=>new
                {
                    Id = p.Id,
                    Picture = p.Picture.Path,
                    User = p.User.Username
                })
                .OrderBy(p=>p.Id)
                .ToList();

            string jsonString = JsonConvert.SerializeObject(posts, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var popularUsers = context.Users
                .Where(u => u.Followers.Count > 0 && u.Followers.Any(f=>f.Follower.Comments.Any(c=>u.Posts.Any(p=>p.Id==c.PostId))))
                .OrderBy(u=>u.Id)
                .Select(u=>new
                {
                    Username = u.Username,
                    Followers = u.Followers.Count
                }
                )
                .ToList();

            //var popularUsers2 = context.Users
            //    .Where(u=>u.Posts.Any(p=>p.Comments.Select(c=>c.UserId).Intersect(u.Followers.Select(f=>f.FollowerId)).Any()))
            //    .ToList();

            string jsonString = JsonConvert.SerializeObject(popularUsers, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {


            var users = context.Users
                .Select(u => new UserByTopPostsDto
                {
                    Username = u.Username,
                    MostComments = u.Posts
                                    .OrderByDescending(p => p.Comments.Count)
                                    .FirstOrDefault() != null
                                    ?
                                    u.Posts
                                    .OrderByDescending(p => p.Comments.Count)
                                    .First()
                                    .Comments.Count
                                    :
                                    0
                })
                .OrderByDescending(u => u.MostComments)
                .ThenBy(u => u.Username)
                .ToArray();

            //var users = context.Users
            //    .Include(u => u.Posts)
            //    .ThenInclude(p=>p.Comments)
            //    .ProjectTo<UserByTopPostsDto>(mapper.ConfigurationProvider)
            //    .OrderByDescending(u => u.MostComments)
            //    .ThenBy(u => u.Username)
            //    .ToArray();

            var xmlNameSpaces = new XmlSerializerNamespaces(new [] { XmlQualifiedName.Empty});

            var serializer = new XmlSerializer(typeof(UserByTopPostsDto[]),new XmlRootAttribute("users"));

           var writer = new StringWriter();

           serializer.Serialize(writer,users,xmlNameSpaces);

            return writer.ToString();            
        }
    }
}
