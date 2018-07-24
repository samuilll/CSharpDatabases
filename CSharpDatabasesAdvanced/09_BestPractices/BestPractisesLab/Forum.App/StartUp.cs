using Forum.Data;
using Forum.Services;
using Forum.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
using AutoMapper.Configuration;
using Forum.App.Models;
using Forum.Data.Models;

namespace Forum.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureService();

            InitializeMapper();

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

          //  serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<ForumProfile>());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }

        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ForumProfile>());
        }

    }
}
