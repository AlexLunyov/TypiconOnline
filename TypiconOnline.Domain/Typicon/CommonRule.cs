﻿using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Правило для описания повторяющихся элементов, которые можно в дальнейшем неоднократно использовать.
    /// Пример: ектении
    /// </summary>
    public class CommonRule : RuleEntity
    {
        /// <summary>
        /// Наименование Общего правила.
        /// Должно быть уникальным в рамках Устава
        /// </summary>
        public virtual string Name { get; set; }

        public virtual List<VariableRuleLink<CommonRule>> VariableLinks { get; set; } = new List<VariableRuleLink<CommonRule>>();

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            base.Validate(serializerRoot);

            if (string.IsNullOrEmpty(RuleDefinition))
            {
                AddBrokenConstraint(new BusinessConstraint("Правило должно быть определено.", nameof(RuleDefinition)));
            }

            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenConstraint(new BusinessConstraint("Общее Правило: Наименование обязательно для заполнения."));
            }
        }
    }
}

