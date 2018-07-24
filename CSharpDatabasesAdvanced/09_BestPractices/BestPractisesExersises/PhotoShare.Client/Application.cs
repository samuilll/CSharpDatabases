namespace PhotoShare.Client
{
    using Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;
    using System;

    public class Application
    {
        public static void Main()
        {
            ResetDatabase();

            IServiceProvider serviceProvider = ConfigureServices();

            Engine engine = new Engine(serviceProvider);

            engine.Run();

            System.Console.WriteLine("Successfull!");
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<PhotoShareContext>(options => options.UseSqlServer(ServerConfig.ConnectionString));
            serviceCollection.AddTransient<ICommandDispatcher, CommandDispatcher>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ITownService, TownService>();
            serviceCollection.AddTransient<ITagService, TagService>();
            serviceCollection.AddTransient<IAlbumService, AlbumService>();

            var provider = serviceCollection.BuildServiceProvider();

            return provider;
        }

        private static void ResetDatabase()
        {
            using (var db = new PhotoShareContext())
            {
                db.Database.EnsureDeleted();
               db.Database.Migrate();

            }
        }
    }
}
