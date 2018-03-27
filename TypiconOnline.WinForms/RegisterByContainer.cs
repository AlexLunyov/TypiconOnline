using System;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EF;
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
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

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
                        scan.AssemblyContainingType<EFUnitOfWork>();
                        scan.AssemblyContainingType<SQLiteUnitOfWork>();
                        scan.AssemblyContainingType<DocxScheduleWeekViewer>(); 
                        scan.WithDefaultConventions();
                    });
                    x.For<IUnitOfWork>().Use<SQLiteUnitOfWork>().SelectConstructor(() => new SQLiteUnitOfWork()).Singleton();
                    //x.For<IUnitOfWork>().Use<EFUnitOfWork>().Singleton();
                    x.For<ITypiconEntityService>().Use<TypiconEntityService>();
                    x.For<IEvangelionContext>().Use<EvangelionContext>();
                    x.For<IApostolContext>().Use<ApostolContext>();
                    x.For<IOldTestamentContext>().Use<OldTestamentContext>();
                    x.For<IPsalterContext>().Use<PsalterContext>();
                    x.For<IOktoikhContext>().Use<OktoikhContext>();
                    x.For<ITheotokionAppContext>().Use<TheotokionAppContext>();
                    x.For<IEasterContext>().Use<EasterContext>(); 
                    x.For<IScheduleService>().Use<ScheduleService>(); 
                    x.For<IRuleSerializerRoot>().Use<RuleSerializerRoot>();
                    x.For<IScheduleWeekViewer>().Use<DocxScheduleWeekViewer>();
                    x.For<IRuleHandlerSettingsFactory>().Use<RuleHandlerSettingsFactory>();
                });
        }
    }
}
