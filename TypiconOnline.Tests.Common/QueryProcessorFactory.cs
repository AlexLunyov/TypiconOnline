using TypiconOnline.Domain.Query;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Tests.Common
{
    public class QueryProcessorFactory
    {
        public static IQueryProcessor Create()
        {
            var container = new SimpleInjector.Container();

            container.RegisterTypiconQueryClasses();

            container.Register(UnitOfWorkFactory.Create, SimpleInjector.Lifestyle.Singleton);

            var processor = container.GetInstance<IQueryProcessor>();

            return processor;
        }
    }
}
