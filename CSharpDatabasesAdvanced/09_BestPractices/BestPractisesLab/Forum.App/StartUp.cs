using Forum.Data;
using Forum.Services;
using Forum.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Forum.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureService();

            var engine = new Engine(serviceProvider);

            engine.Run();

        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IUserService,UserService>();
            serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();

            serviceCollection.AddTransient<IPostService, PostService>();
            serviceCollection.AddTransient<IReplyService, ReplyService>();
            serviceCollection.AddTransient<ICategoryService, CategoryService >();

            serviceCollection.AddDbContext<ForumDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(Configuration.ConnectionbString));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
