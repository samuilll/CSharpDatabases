using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using System.ComponentModel.DataAnnotations;
using Instagraph.DataProcessor.Dtos;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Error: Invalid data.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var picturesToValidate = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            var validPictures = new List<Picture>();

            foreach (var picToValidate in picturesToValidate)
            {
                if (!IsValid(picToValidate)
                    || validPictures.Any(vp => vp.Path == picToValidate.Path)
                    || string.IsNullOrEmpty(picToValidate.Path))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                validPictures.Add(picToValidate);

                sb.AppendLine($"Successfully imported Picture {picToValidate.Path}.");
            }

            context.Pictures.AddRange(validPictures);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');

        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var usersToValidate = JsonConvert.DeserializeObject<UserToImportDto[]>(jsonString);

            var validUsers = new List<User>();

            var pictures = context.Pictures.ToList();

            foreach (var userToValidate in usersToValidate)
            {
                var picture = GetThePicture(pictures,userToValidate.ProfilePicture);

                if (picture==null||!IsValid(userToValidate))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var validUser = new User
                {
                    Username = userToValidate.Username,
                    Password = userToValidate.Password,
                    ProfilePicture = picture
                };

                validUsers.Add(validUser);

                sb.AppendLine($"Successfully imported User {validUser.Username}.");
            }

            context.Users.AddRange(validUsers);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');
        }

        private static Picture GetThePicture(List<Picture> pictures, string profilePicturePath)
        {
            return pictures.FirstOrDefault(p=>p.Path ==profilePicturePath);
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var users = context.Users.ToList();

            var usersFollowers = JsonConvert.DeserializeObject<FollowerToImportDto[]>(jsonString);

            var validUserFollowers = new List<UserFollower>();

            foreach (var userFollower in usersFollowers)
            {
                var user = GetUserOrNull(users, userFollower.User);

                var follower = GetUserOrNull(users, userFollower.Follower);

                if (user==null || follower==null || validUserFollowers.Any(uf=>(uf.UserId==user.Id && uf.FollowerId==follower.Id)))                                                                        
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var validUserFollower = new UserFollower()
                {
                    UserId = user.Id,
                    FollowerId = follower.Id
                };
                ;
                validUserFollowers.Add(validUserFollower);

                sb.AppendLine($"Successfully imported Follower {follower.Username} to User {user.Username}.");
            }

            context.UsersFollowers.AddRange(validUserFollowers);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');
        }

        private static User GetUserOrNull(List<User> users, string userUsername)
        {
            return users.FirstOrDefault(u=>u.Username==userUsername);
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

           // XmlReader reader =;

            var serializer = new XmlSerializer(typeof(PostForImportDto[]),new XmlRootAttribute("posts"));

            var postsToValidate = (PostForImportDto[])serializer.Deserialize(new XmlTextReader(new StringReader(xmlString)));

            var validPosts = new List<Post>();

            var pictures = context.Pictures.ToList();

            var users = context.Users.ToList();

              foreach (var postToValidate in postsToValidate)
             {
                 var picture = GetThePicture(pictures, postToValidate.PicturePath);

                var user = GetUserOrNull(users,postToValidate.UserUserName);

                if (picture == null||user==null || !IsValid(postToValidate))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var validPost = new Post
                {
                   Caption = postToValidate.Caption,
                   UserId = user.Id,
                   PictureId = picture.Id
                };

                validPosts.Add(validPost);


                    sb.AppendLine($"Successfully imported Post {validPost.Caption}.");
                }

                context.Posts.AddRange(validPosts);

                context.SaveChanges();

                return sb.ToString().TrimEnd('\n', '\r');
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(CommentToImportDto[]), new XmlRootAttribute("comments"));

            var commentsToValidate = (CommentToImportDto[])serializer.Deserialize(new XmlTextReader(new StringReader(xmlString)));

            var validComments = new List<Comment>();

            var posts = context.Posts.ToList();

            var users = context.Users.ToList();

            foreach (var commentToValidate in commentsToValidate)
            {
                if (commentToValidate.Post==null)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var post = GetPostOrNull(posts, commentToValidate.Post.Id);

                var user = GetUserOrNull(users, commentToValidate.User);

                if (post == null || user == null || !IsValid(commentsToValidate))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var validComment = new Comment
                {
                    Content = commentToValidate.Content,
                    UserId = user.Id,
                    PostId = post.Id
                };

                validComments.Add(validComment);


                sb.AppendLine($"Successfully imported Comment {validComment.Content}.");
            }

            context.Comments.AddRange(validComments);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');
        }

        private static Post GetPostOrNull(List<Post> posts, int id)
        {
            return posts.SingleOrDefault(p=>p.Id==id);
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);

            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}
