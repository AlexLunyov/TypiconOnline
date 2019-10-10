using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Tests.Common
{
    public class QueryProcessorFactory
    {
        public static IQueryProcessor Create()
        {
            var container = new SimpleInjector.Container();

            container.Register(typeof(IQueryHandler<,>), typeof(QueryProcessor).Assembly, typeof(TypiconEntityModel).Assembly);
            container.Register<IQueryProcessor, QueryProcessor>();

            container.Register(UnitOfWorkFactory.Create, SimpleInjector.Lifestyle.Singleton);

            var processor = container.GetInstance<IQueryProcessor>();

            return processor;
        }
    }
}
