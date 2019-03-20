using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Абстрактный класс с определнием Правила для вычисления ModifiedRule
    /// Используется при вычислении ModifiedYear
    /// </summary>
    public abstract class RuleEntityToModify : RuleEntity
    {
        IRuleElement _ruleToModify;
        public string RuleToModifyDefinition { get; set; }

        public virtual T GetRuleToModify<T>(IRuleSerializerRoot serializerRoot) where T : IRuleElement
        {
            return InnerGetRule<T>(ref _ruleToModify, serializerRoot, RuleToModifyDefinition);
        }
    }
}
