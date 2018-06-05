using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Query;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore;

namespace TypiconOnline.Tests.Common
{
    public class DataQueryProcessorFactory
    {
        public static IDataQueryProcessor Create() => Create(UnitOfWorkFactory.Create());

        public static IDataQueryProcessor Create(IUnitOfWork unitOfWork)
        {
            var container = new SimpleInjector.Container();

            container.RegisterTypiconQueryClasses();

            container.RegisterInstance(unitOfWork);

            var processor = container.GetInstance<IDataQueryProcessor>();

            return processor;
        }
    }
}
