using System;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using StructureMap;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.WinServices;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Repository.EFCore;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Repository.EFCore.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace TypiconOnline.WinForms
{
    public class RegisterByContainer
    {
        public IContainer Container;

        public RegisterByContainer()
        {
            Container = new Container(x => 
                {
                    x.Scan(scan =>
                    {
                        scan.TheCallingAssembly();
                        scan.AssemblyContainingType<ITypiconEntityService>();
                        scan.AssemblyContainingType<TypiconEntity>();
                        scan.AssemblyContainingType<IAggregateRoot>();
                        scan.AssemblyContainingType<UnitOfWork>();
                        scan.AssemblyContainingType<DocxScheduleWeekViewer>(); 
                        scan.WithDefaultConventions();
                    });

                    
                    var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

                    //MSSql connection
                    //var connectionString = ConfigurationManager.ConnectionStrings["DBTypicon"].ConnectionString;
                    //optionsBuilder.UseSqlServer(connectionString);

                    //SQLite connection
                    var connectionString = @"FileName=data\SQLiteDB.db";
                    optionsBuilder.UseSqlite(connectionString);

                    //x.ForConcreteType<TypiconDBContext>()
                    //    .Configure
                    //    .Ctor<DbContextOptions<TypiconDBContext>>("options").Is(optionsBuilder.Options)
                    //    .Singleton();

                    x.For<TypiconDBContext>()
                        .Use<CachedDbContext>()
                        .Ctor<DbContextOptions<TypiconDBContext>>("options").Is(optionsBuilder.Options)
                        .Singleton();

                    x.For<IRepositoryFactory>().Use<RepositoryFactory>();
                    //x.ForConcreteType<SQLiteDBContext>().Configure.Singleton();
                    x.For<IUnitOfWork>().Use<UnitOfWork>()
                        //.SelectConstructor(() => new UnitOfWork(dbContext))
                        .Singleton(); 
                    
                    //x.For<IUnitOfWork>().Use<EFUnitOfWork>().Singleton();
                    x.For<ITypiconEntityService>().Use<TypiconEntityService>();
                    x.For<IEvangelionContext>().Use<EvangelionContext>();
                    x.For<IApostolContext>().Use<ApostolContext>();
                    x.For<IOldTestamentContext>().Use<OldTestamentContext>();
                    x.For<IPsalterContext>().Use<PsalterContext>();
                    x.For<IOktoikhContext>().Use<OktoikhContext>();
                    x.For<ITheotokionAppContext>().Use<TheotokionAppContext>();
                    x.For<IEasterContext>().Use<EasterContext>();
                    x.For<IModifiedYearFactory>().Use<ModifiedYearFactory>();
                    x.For<IRulesExtractor>().Use<RulesExtractor>();
                    x.For<IScheduleService>().Use<ScheduleService>(); 
                    x.For<IRuleSerializerRoot>().Use<RuleSerializerRoot>();
                    x.For<IScheduleWeekViewer>().Use<DocxScheduleWeekViewer>();
                    x.For<IRuleHandlerSettingsFactory>().Use<RuleHandlerSettingsFactory>();
                });
        }
    }
}
