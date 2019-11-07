using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class TriodionRule : DayRule
    {
        public virtual int DaysFromEaster { get; set; }
        /// <summary>
        /// Означает, что правило будет добавляться в качестве дополнения к Правилу Минеи 
        /// при формировании последовательностей богослужений.
        /// Используется при формировании настроек для обработчика Правил
        /// </summary>
        public bool IsTransparent { get; set; }

        /// <summary>
        /// Список на используемые в данном Правиле Переменные Устава
        /// </summary>
        public virtual List<VariableModRuleLink<TriodionRule>> VariableLinks { get; set; }

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            base.Validate(serializerRoot);

            if (DayRuleWorships.Count > 1)
            {
                AddBrokenConstraint(new BusinessConstraint("В коллекции Текстов служб не должно быть более одного Текста служб", "TriodionRule"));
            }
        }
    }
}
