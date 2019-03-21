using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Абстрактный класс с определением Правила для вычисления ModifiedRule
    /// Используется при вычислении ModifiedYear
    /// </summary>
    public abstract class ModRuleEntity : RuleEntity
    {
        IRuleElement _modRule;
        private string _modRuleDefinition;

        /// <summary>
        /// Определение Правила для переходящих праздников
        /// </summary>
        public string ModRuleDefinition
        {
            get => _modRuleDefinition;
            set
            {
                _modRuleDefinition = value;
                _modRule = null;
            }
        }

        public virtual T GetModRule<T>(IRuleSerializerRoot serializerRoot) where T : IRuleElement
        {
            return InnerGetRule<T>(ref _modRule, serializerRoot, ModRuleDefinition);
        }
    }
}
