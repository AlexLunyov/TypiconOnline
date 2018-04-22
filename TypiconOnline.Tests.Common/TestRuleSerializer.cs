using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Tests.Common
{
    public static class TestRuleSerializer
    {
        public static T Deserialize<T>(string description) where T: RuleElement
        {
            var serializer = new RuleSerializerRoot(BookStorageFactory.Create());

            return serializer.Container<T>().Deserialize(description);
        }

        public static IRuleSerializerRoot Root
        {
            get
            {
                return new RuleSerializerRoot(BookStorageFactory.Create());
            }
        }
    }
}
