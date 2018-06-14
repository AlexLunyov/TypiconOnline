using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Tests.Common
{
    public class DataQueryProcessorFactory
    {
        private static IDataQueryProcessor queryProcessor;
        public static IDataQueryProcessor Instance
        {
            get
            {
                if (queryProcessor == null)
                {
                    queryProcessor = Create();
                }

                return queryProcessor;
            }
        }
        public static IDataQueryProcessor Create() => Create(UnitOfWorkFactory.Create());

        public static IDataQueryProcessor Create(IUnitOfWork unitOfWork)
        {
            var container = new SimpleInjector.Container();

            container.Register<ITypiconSerializer, TypiconSerializer>();

            container.RegisterTypiconQueryClasses();

            container.RegisterInstance(unitOfWork);

            var processor = container.GetInstance<IDataQueryProcessor>();

            return processor;
        }
    }
}
