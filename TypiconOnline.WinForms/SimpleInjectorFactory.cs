using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.WinServices;
using TypiconOnline.WinServices.Implementations;
using TypiconOnline.WinServices.Interfaces;

namespace TypiconOnline.WinForms
{
    public class SimpleInjectorFactory
    {
        public static Container Create()
        {
            var container = new Container();

            InitRepository(container);

            InitQueries(container);

            InitServices(container);

            return container;
        }
        private static void InitRepository(Container container)
        {
            container.Register<IRepositoryFactory, RepositoryFactory>(Lifestyle.Singleton);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);
            container.Register<TypiconDBContext, TypiconDBContext>(Lifestyle.Singleton);

            container.Register(CreateDbContextOptions, Lifestyle.Singleton);
        }

        private static void InitQueries(Container container)
        {
            container.RegisterTypiconQueryClasses();
        }

        private static void InitServices(Container container)
        {
            container.Register<ITypiconVersionService, TypiconVersionService>();
            container.Register<IScheduleService, ScheduleService>();
            container.Register<IScheduleDayNameComposer, ScheduleDayNameComposer>();
            container.Register<ITypiconSerializer, TypiconSerializer>();
            container.Register<IRuleSerializerRoot, RuleSerializerRoot>();
            container.Register<IRuleHandlerSettingsFactory, RuleHandlerSettingsFactory>();
            container.Register<IScheduleDataCalculator, ScheduleDataCalculator>();
            container.Register<IModifiedRuleService, ModifiedRuleService>(); 
            container.Register<IModifiedYearFactory, ModifiedYearFactory>(); 
            container.Register<IDocxTemplateService, DocxTemplateService>();
            container.Register<ITypiconFacade, TypiconFromEntityFacade>();
        }

        private static DbContextOptions<TypiconDBContext> CreateDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            //MSSql connection
            //var connectionString = ConfigurationManager.ConnectionStrings["DBTypicon"].ConnectionString;
            //optionsBuilder.UseSqlServer(connectionString);

            //SQLite connection
            var connectionString = @"FileName=data\SQLiteDB.db";
            optionsBuilder.UseSqlite(connectionString);

            return optionsBuilder.Options;
        }
    }
}
