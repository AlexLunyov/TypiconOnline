using System;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EF;
using StructureMap;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EFCore;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Serialization;

namespace TypiconMigrationTool
{
    public class RegisterByContainer
    {
        public IContainer Container;

        public RegisterByContainer()
        {

            Container = new Container(x => {
                x.For<IUnitOfWork>().Use<EFUnitOfWork>();

            });
            Container = new Container(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.AssemblyContainingType<ITypiconEntityService>();
                    scan.AssemblyContainingType<TypiconEntity>();
                    scan.AssemblyContainingType<IAggregateRoot>();
                    scan.AssemblyContainingType<EFUnitOfWork>();
                    scan.AssemblyContainingType<UnitOfWork>();
                    scan.WithDefaultConventions();
                });
                x.For<IUnitOfWork>().Use<EFUnitOfWork>().SelectConstructor(() => new EFUnitOfWork()).Singleton();
                //x.For<IUnitOfWork>().Use<EFUnitOfWork>();
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
            });
        }
    }
}
