using AutoMapper;
using Employees.Contracts;
using Employees.Services;
using Employees.Services.Contracts;
using Employess.App;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Employees.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var engine = new Engine(serviceProvider);

            engine.Run();
        }

        //private static void InitializeMapper()
        //{
        //    Mapper.Initialize(cfg => cfg.AddProfile<ProfileMapping>());
        //}

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IEmployeeService, EmployeeService>();

            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

            services.AddSingleton<IDatabaseInitializeService, DatabaseInitializeService>();

            //services.AddAutoMapper(cfg => cfg.AddProfile<ProfileMapping>());

            var mapper = CreateConfiguration().CreateMapper();

            var config = CreateConfiguration();

            services.AddSingleton<IMapper>(mapper);

            services.AddSingleton<IConfigurationProvider>(config);

            var provider = services.BuildServiceProvider();

            return provider;
        }

        private static IConfigurationProvider CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(ProfileMapping));
            });

            return config;
        }
    }
}
