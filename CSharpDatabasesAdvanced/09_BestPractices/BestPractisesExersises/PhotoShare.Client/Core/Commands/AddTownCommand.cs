namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using PhotoShare.Client.Core.Commands.Contracts;
    using System;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;

    public class AddTownCommand:ICommand
    {
        private readonly ITownService townService;

        private readonly string successResult = "Town {0} was added successfully!";


        public AddTownCommand(ITownService townService)
        {
            this.townService = townService;
        }

        public string Execute(string[] data)
        {
            if (!Session.HasLoggedUser())
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            var town = this.townService.Create(data);

            return string.Format(successResult,town.Name);        
        }
    }
}
