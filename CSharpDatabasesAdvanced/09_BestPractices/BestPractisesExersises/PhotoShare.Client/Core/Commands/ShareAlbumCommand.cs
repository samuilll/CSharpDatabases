namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;
    using System;

    public class ShareAlbumCommand:ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;

        public ShareAlbumCommand(IAlbumService albumService, IUserService userService)
        {
            this.albumService = albumService;
            this.userService = userService;
        }

        public string Execute(string[] commandArgs)
        {
            var albumId = int.Parse(commandArgs[0]);
            var username = commandArgs[1];
            var permission = commandArgs[2];


            if (!Session.HasLoggedUser())
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            var credentialUser = Session.User;

            return this.albumService.ShareAlbum(albumId,username,permission, credentialUser);
        }
    }
}
