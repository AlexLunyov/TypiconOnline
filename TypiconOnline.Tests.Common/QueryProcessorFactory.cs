using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Tests.Common
{
    public class QueryProcessorFactory
    {
        public static IQueryProcessor Create()
        {
            var container = new SIContainer();

            var processor = container.GetInstance<IQueryProcessor>();

            return processor;
        }
    }
}
