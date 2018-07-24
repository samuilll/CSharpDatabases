using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Services
{
    using Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System.Linq;

    public class AlbumService : IAlbumService
    {
        private const string successShareMessage = "Username {0} added to album {1} ({2})";

        private const string successUploadMessage = "Picture {0} added to {1}!";


        private readonly PhotoShareContext context;

        private IUserService userService;

        public AlbumService(PhotoShareContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public void AddTagTo(string albumName,string tagName, User credentialUser)
        {

            var album = context.Albums.SingleOrDefault(a => a.Name == albumName);

            var tag = context.Tags.SingleOrDefault(t => t.Name == tagName);

            if (album==null||tag==null)
            {
                throw new ArgumentException(ExeptionMessageHandler.AlbumAlreadyExistExeption);
            }

            var albumId = album.Id;

            var isAuthorized = credentialUser.AlbumRoles.Any(ar => ar.AlbumId == albumId && ar.Role == Role.Owner);

            if (!isAuthorized)
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            context.AlbumTags.Add(new AlbumTag()
            {
                Album = album,
                Tag = tag
            });

            context.SaveChanges();
        }

        public Album Create(string username,string albumTitle,string color, string[] tags)
        {


            if (!Enum.TryParse<Color>(color, out Color bgColor))
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.ColorNotFount, color));
            }

            if (this.Exist(albumTitle))
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.AlbumAlreadyExistExeption, albumTitle));
            }

            var album = new Album()
            {
                Name = albumTitle,
                BackgroundColor = bgColor
            };

            context.SaveChanges();

            foreach (var tag in context.Tags.Where(t=>tags.Contains(t.Name)).ToList())
            {
                context.AlbumTags.Add(new AlbumTag()
                {
                     Album = album,
                     Tag=tag
                });
            }

            var user =this.userService.GetByUsername(username);

            context.AlbumRoles.Add(new AlbumRole()
            {
                 Album=album,
                  User = user,
                  Role = Role.Owner
            });

            context.SaveChanges();

            return album;
    }

        public bool Exist(string title)
        {
            return this.context.Albums.Any(a=>a.Name==title);
        }

        public Album GetById(int id)
        {
            var album = context.Albums.SingleOrDefault(a=>a.Id==id);

            if (album==null)
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.AlbumDoesNotExistExeption,id.ToString()));
            }

            return album;
        }

        public Album GetByName(string albumName)
        {
            var album = context.Albums.FirstOrDefault(a => a.Name == albumName);

            if (album == null)
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.AlbumDoesNotExistExeption, albumName));
            }

            return album;
        }

        public string ShareAlbum(int albumId, string username, string permissionAsString,User credentialUser)
        {
            var album = this.GetById(albumId);

            var user = this.userService.GetByUsername(username);

            var correctPermission = Enum.TryParse<Role>(permissionAsString, out Role permission);

            var isAuthorized = credentialUser.AlbumRoles.Any(ar => ar.AlbumId == albumId && ar.Role == Role.Owner);

            if (!isAuthorized)
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            if (!correctPermission)
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.PermissionExeption));
            }

            var existinfAlbumRole = context.AlbumRoles.SingleOrDefault(ar => ar.UserId == user.Id && ar.AlbumId == albumId);

            if (existinfAlbumRole!=null)
            {
                existinfAlbumRole.Role = permission;

                context.SaveChanges();

                return string.Format(successShareMessage, username, permissionAsString, album.Name);
            }

            context.AlbumRoles.Add(new AlbumRole()
            {
                User=user,
                Album=album,
                Role=permission
            });

            context.SaveChanges();
            return string.Format(successShareMessage, username, permissionAsString, album.Name);

        }

        public string UploadPicture(string albumName, string pictureTitle, string filePath,User credentialUser)
        {
            var album = this.GetByName(albumName);

            var albumId = album.Id;

            var picture = new Picture()
            {
                Album = album,
                Title = pictureTitle,
                Path=filePath
            };

            var isAuthorized = credentialUser.AlbumRoles.Any(ar => ar.AlbumId == albumId && ar.Role == Role.Owner);

            if (!isAuthorized)
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            context.Pictures.Add(picture);
            context.SaveChanges();

            return string.Format(successUploadMessage,pictureTitle,albumName);
        }
    }
}
