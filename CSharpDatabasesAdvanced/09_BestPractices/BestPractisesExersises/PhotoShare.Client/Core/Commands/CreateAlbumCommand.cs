namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;
    using System;
    using System.Linq;

    public class CreateAlbumCommand:ICommand
    {
        private readonly IUserService userService;

        private readonly IAlbumService albumService;

        private readonly ITagService tagService;

        private const string successMessage = "Album {0} successfully created!";

        public CreateAlbumCommand(IUserService userService, IAlbumService albumService, ITagService tagService)
        {
            this.userService = userService;
            this.albumService = albumService;
            this.tagService = tagService;
        }



        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            var username = data[0];

            var albumTitle = data[1];

            var color = data[2];

            var tags = data.Skip(3).Select(t=>t="#"+t).ToArray();

            bool hasCredentials = Session.HasCredentials(username);

            if (!hasCredentials)
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }


            foreach (var tagName in tags)
            {
                if (!this.tagService.Exist(tagName))
                {
                    throw new ArgumentException(string.Format(ExeptionMessageHandler.InvalidTags));
                }
            }

            var user = this.userService.GetByUsername(username);

            var album = this.albumService.Create( username,  albumTitle,  color, tags);

            return string.Format(successMessage, album.Name);
        }
    }
}
