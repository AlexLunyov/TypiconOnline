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
using TypiconOnline.Repository.EFSQLite;

namespace ScheduleForm
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
                        scan.AssemblyContainingType<EFSQLiteUnitOfWork>();
                        scan.AssemblyContainingType<DocxScheduleWeekViewer>(); 
                        scan.WithDefaultConventions();
                    });
                    x.For<IUnitOfWork>().Use<EFSQLiteUnitOfWork>().SelectConstructor(() => new EFSQLiteUnitOfWork());
                    //x.For<IUnitOfWork>().Use<EFSQLiteUnitOfWork>();//.SelectConstructor(() => new EFSQLiteUnitOfWork(@"Data\SQLiteDB.db"));
                    x.For<ITypiconEntityService>().Use<TypiconEntityService>();
                    x.For<IEvangelionService>().Use<EvangelionService>();
                    x.For<IApostolService>().Use<ApostolService>();
                    x.For<IOldTestamentService>().Use<OldTestamentService>();
                    x.For<IPsalterService>().Use<PsalterService>();
                    x.For<IOktoikhContext>().Use<OktoikhContext>();
                    x.For<ITheotokionAppContext>().Use<TheotokionAppContext>();
                    x.For<IEasterContext>().Use<EasterContext>();
                });
        }
    }
}
