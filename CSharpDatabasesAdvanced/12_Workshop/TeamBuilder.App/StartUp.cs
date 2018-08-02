using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TeamBuilder.App.Contracts;
using TeamBuilder.Models;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App
{
    class StartUp
    {
        public static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

             var engine = new Engine(serviceProvider);

             engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IDatabaseInitializeService, DatabaseInitializeService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<ITeamService, TeamService>();


            var mapper = CreateConfiguration();

            services.AddSingleton<IMapper>(mapper);

            var provider = services.BuildServiceProvider();

            return provider;
        }

        private static IMapper CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(ProfileMapping));
            }).CreateMapper();

            return config;
        }
    }
}

