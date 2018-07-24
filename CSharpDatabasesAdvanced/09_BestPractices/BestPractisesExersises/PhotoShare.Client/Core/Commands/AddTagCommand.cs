namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using Utilities;
    using PhotoShare.Services.Contracts;
    using PhotoShare.Client.Core.Commands.Contracts;
    using System;
    using PhotoShare.Services;

    public class AddTagCommand:ICommand
    {
        private ITagService tagService;
        private const string successMessage = " Tag {0} was added successfully to database!";

        public AddTagCommand(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public string Execute(string[] data)
        {
            var tagName = data[0];

            if (!Session.HasLoggedUser())
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            this.tagService.Create(tagName);

            return string.Format(successMessage,"#"+tagName);
        }
    }
}
