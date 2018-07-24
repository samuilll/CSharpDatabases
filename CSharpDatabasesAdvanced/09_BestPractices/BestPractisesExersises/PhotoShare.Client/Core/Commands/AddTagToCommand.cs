namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;
    using System;

    public class AddTagToCommand :ICommand
    {
        private readonly IAlbumService albumService;

        private const string successMessage = "Tag {0} added to {1}!";

        public AddTagToCommand(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        public string Execute(string[] data)
        {
            var albumName = data[0];
            var tagName ="#"+ data[1];

            if (!Session.HasLoggedUser())
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            var credentialUser = Session.User;

            this.albumService.AddTagTo(albumName, tagName,credentialUser);

            return string.Format(successMessage, tagName, albumName);
        }
    }
}
