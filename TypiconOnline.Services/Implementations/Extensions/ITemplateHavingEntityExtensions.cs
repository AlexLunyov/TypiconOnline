using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.AppServices.Implementations.Extensions
{
    public static class ITemplateHavingEntityExtensions
    {
        /// <summary>
        /// Возвращает первое Правило с имеющимся определением из цепочки шаблонов, либо NULL
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public static (ITemplateHavingEntity existingRule, RootContainer container) GetFirstExistingRule(this ITemplateHavingEntity rule, IRuleSerializerRoot serializer)
        {
            ITemplateHavingEntity r = null;
            var cont = rule.GetRule<RootContainer>(serializer);

            if (cont != null)
            {
                r = rule;
            }
            else if (rule.Template != null)
            {
                return GetFirstExistingRule(rule.Template, serializer);
            }

            return (r, cont);
        }
    }
}
