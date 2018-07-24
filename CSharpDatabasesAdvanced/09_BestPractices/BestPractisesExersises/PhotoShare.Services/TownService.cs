namespace PhotoShare.Services
{
    using Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class TownService : ITownService
    {
        private readonly PhotoShareContext context;

        public TownService(PhotoShareContext context)
        {
            this.context = context;
        }
        public Town Create(string[] arguments)
        {
            string townName = arguments[0];
            string country = arguments[1];

            Town town = new Town
            {
                Name = townName,
                Country = country
            };

                if (context.Towns.Any(t => t.Name == townName))
                {
                    throw new ArgumentException(string.Format(ExeptionMessageHandler.TownAlreadyAddedExeption,townName));
                }

                context.Towns.Add(town);
                context.SaveChanges();
            
            return town;
        }

        public Town GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Town GetByName(string townName)
        {
            Town town;

                town = context.Towns.SingleOrDefault(t => t.Name == townName);

            if (town == null)
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.TownNotFoundExeption,townName));
            }

            return town;
        }
    }
}
