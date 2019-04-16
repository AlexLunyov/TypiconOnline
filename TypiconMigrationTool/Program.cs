using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleHandling;
using System;
using System.IO;
using System.Threading.Tasks;
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
            //try
            //{
            //var container = new RegisterByContainer().Container;

            //var unitOfWork = container.GetInstance<IUnitOfWork>();

            var folderPath = Properties.Settings.Default.FolderPath;

            while (true)
            {
                Console.WriteLine("Что желаете? '1' - миграция БД MS SQL, '2' - миграция БД SQLite, '3' - миграция БД PostgreSQL, '4' - миграция БД MySQL");

                ConsoleKeyInfo info = Console.ReadKey();

                Task task = null;

                switch (info.KeyChar)
                {
                    case '1':
                        {
                            var (uof, db, options) = uofFactory.GetMSSqlUnitOfWork();
                            task = Migrate(uof, db, options, folderPath);
                        }
                        break;
                    case '2':
                        {
                            var (uof, db, options) = uofFactory.GetSQLiteUnitOfWork();
                            task = Migrate(uof, db, options, folderPath);
                        }
                        break;
                    case '3':
                        {
                            var (uof, db, options) = uofFactory.GetPostgreSQLUnitOfWork();
                            task = Migrate(uof, db, options, folderPath);
                        }
                        break;
                    case '4':
                        {
                            var (uof, db, options) = uofFactory.GetMySQLUnitOfWork();
                            task = Migrate(uof, db, options, folderPath);
                        }
                        break;
                }

                if (task != null)
                {
                    task.Wait();
                }


            }


            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //} 


            //FillDB(unitOfWork);

            //unitOfWork.Commit();
        }



        private static async Task Migrate(IUnitOfWork unitOfWork, TypiconDBContext dbContext, Action<DbContextOptionsBuilder> optionsBuilder, string folderPath)
        {
            ScheduleHandler sh = new ScheduleHandler("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\data\\ScheduleDB.mdb;");

            var service = GetUserCreationService(optionsBuilder);

            TypiconMigration migration = new TypiconMigration(unitOfWork, dbContext, sh, service, folderPath);

            await migration.Execute();
        }


        private static UserCreationService GetUserCreationService(Action<DbContextOptionsBuilder> optionsBuilder)
        {
            var services = new ServiceCollection();

            //setup our DI
            // Add framework services.            
            services.AddDbContext<TypiconDBContext>(options => optionsBuilder(options));

            services.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<TypiconDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserCreationService>();

            // Build the IoC from the service collection
            var provider = services.BuildServiceProvider();

            return provider.GetService<UserCreationService>();
        }

    }
}
