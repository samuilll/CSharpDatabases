namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;
    using System;

    public class UploadPictureCommand:ICommand
    {
        private IAlbumService albumService;

        public UploadPictureCommand(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        public string Execute(string[] data)
        {
            var albumName = data[0];
            var picureTitle = data[1];
            var filePath = data[2];


            if (!Session.HasLoggedUser())
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            var credentialUser = Session.User;

            return this.albumService.UploadPicture(albumName, picureTitle, filePath, credentialUser);
        }
    }
}
