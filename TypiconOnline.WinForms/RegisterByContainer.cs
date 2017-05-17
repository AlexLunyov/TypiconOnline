using System;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EF;
using StructureMap;

namespace ScheduleForm
{
    public class RegisterByContainer
    {
        public IContainer Container;

        public RegisterByContainer()
        {

            Container = new Container(x => {
                x.For<IUnitOfWork>().Use<EFUnitOfWork>();

            });
        }
    }
}
