using System;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EF;
using StructureMap;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.WinServices;

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
                        scan.AssemblyContainingType<DocxScheduleWeekViewer>(); 
                        scan.WithDefaultConventions();
                    });
                    x.For<IUnitOfWork>().Use<EFUnitOfWork>();
                    x.For<ITypiconEntityService>().Use<TypiconEntityService>();
                });
        }
    }
}
