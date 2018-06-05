using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Tests.Common
{
    public static class TestRuleSerializer
    {
        public static T Deserialize<T>(string description) where T: RuleElement
        {
            var serializer = Create();

            return serializer.Container<T>().Deserialize(description);
        }

        public static IRuleSerializerRoot Create() => Create(UnitOfWorkFactory.Create());

        public static IRuleSerializerRoot Create(IUnitOfWork unitOfWork)
        {
            return new RuleSerializerRoot(DataQueryProcessorFactory.Create(unitOfWork));
        }
    }
}
