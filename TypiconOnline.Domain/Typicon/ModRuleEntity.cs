using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

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

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            base.Validate(serializerRoot);

            if (string.IsNullOrEmpty(RuleDefinition))
            {
                AddBrokenConstraint(new BusinessConstraint("Правило для переноса служб должно быть определено.", "ModRuleDefinition"));
            }
            else
            {
                var element = GetModRule<IRuleElement>(serializerRoot);
                if (element == null)
                {
                    AddBrokenConstraint(new BusinessConstraint("Правило дял переноса служб заполнено с неопределяемыми системой ошибками.", "ModRuleDefinition"));
                }
                else if (!element.IsValid)
                {
                    AppendAllBrokenConstraints(element.GetBrokenConstraints(), "ModRuleDefinition");
                }
            }
        }
    }
}
