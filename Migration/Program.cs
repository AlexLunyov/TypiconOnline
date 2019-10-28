using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleHandling;
using System;
using System.IO;
using TypiconMigrationTool.Typicon;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconMigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var uofFactory = new UofFactory();


            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var folderPath = config.GetValue<string>("folderpath");

            while (true)
            {
                Console.WriteLine("Что желаете? '1' - миграция БД MS SQL, '2' - миграция БД SQLite, '3' - миграция БД PostgreSQL, '4' - миграция БД MySQL");

                ConsoleKeyInfo info = Console.ReadKey();

                switch (info.KeyChar)
                {
                    case '1':
                        {
                            var (uof, db, options) = uofFactory.GetMSSqlUnitOfWork(config);
                            Migrate(uof, db, options, folderPath, config);
                        }
                        break;
                    case '2':
                        {
                            var (uof, db, options) = uofFactory.GetSQLiteUnitOfWork(config);
                            Migrate(uof, db, options, folderPath, config);
                        }
                        break;
                    case '3':
                        {
                            var (uof, db, options) = uofFactory.GetPostgreSQLUnitOfWork(config);
                            Migrate(uof, db, options, folderPath, config);
                        }
                        break;
                    case '4':
                        {
                            var (uof, db, options) = uofFactory.GetMySQLUnitOfWork(config);
                            Migrate(uof, db, options, folderPath, config);
                        }
                        break;
                }
            }
        }

        private static void Migrate(IUnitOfWork uof, TypiconDBContext dbContext, Action<DbContextOptionsBuilder, IConfiguration> optionsBuilder
            , string folderPath, IConfiguration config)
        {
            ScheduleHandler sh = new ScheduleHandler(config.GetConnectionString("Ole"));

            var service = GetUserCreationService(optionsBuilder, config);

            TypiconMigration migration = new TypiconMigration(uof, dbContext, sh, service, folderPath);

            migration.Execute();
        }


        private static UserCreationService GetUserCreationService(Action<DbContextOptionsBuilder, IConfiguration> optionsBuilder, IConfiguration config)
        {
            var services = new ServiceCollection();

            //setup our DI
            // Add framework services.            
            services.AddDbContext<TypiconDBContext>(options => optionsBuilder(options, config));

            services.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<TypiconDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserCreationService>();
            
            services.AddLogging();

            // Build the IoC from the service collection
            var provider = services.BuildServiceProvider();

            return provider.GetService<UserCreationService>();
        }
    }
}
