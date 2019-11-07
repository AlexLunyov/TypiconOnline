using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Правило, которое для конкретной даты явно описывает особенности богослужения.
    /// Используется как AsAddition
    /// </summary>
    public class ExplicitAddRule : RuleEntity
    {
        /// <summary>
        /// Конкретная дата
        /// </summary>
        public DateTime Date { get; set; }

        public virtual List<VariableRuleLink<ExplicitAddRule>> VariableLinks { get; set; } = new List<VariableRuleLink<ExplicitAddRule>>();

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            if (string.IsNullOrEmpty(RuleDefinition))
            {
                AddBrokenConstraint(new BusinessConstraint("Правило должно быть определено.", nameof(RuleDefinition)));
            }
        }
    }
}
