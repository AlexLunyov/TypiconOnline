using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Variable
{
    public class TypiconVariable : ITypiconVersionChild
    {
        public int Id { get; set; }

        /// <summary>
        /// Id Устава (TypiconVersion)
        /// </summary>
        public virtual int TypiconVersionId { get; set; }

        public virtual TypiconVersion TypiconVersion { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public VariableType Type { get; set; }

        public virtual List<VariableRuleLink<CommonRule>> CommonRuleLinks { get; set; } = new List<VariableRuleLink<CommonRule>>();
        public virtual List<VariableModRuleLink<Sign>> SignLinks { get; set; } = new List<VariableModRuleLink<Sign>>();
        public virtual List<VariableModRuleLink<MenologyRule>> MenologyRuleLinks { get; set; } = new List<VariableModRuleLink<MenologyRule>>();
        public virtual List<VariableModRuleLink<TriodionRule>> TriodionRuleLinks { get; set; } = new List<VariableModRuleLink<TriodionRule>>();
        public virtual List<VariableRuleLink<ExplicitAddRule>> ExplicitAddRuleLinks { get; set; } = new List<VariableRuleLink<ExplicitAddRule>>();

        public void AddLink<T>(T entity, DefinitionType definitionType = DefinitionType.Rule) where T : RuleEntity//, new()
        {
            if (entity is CommonRule)
            {
                CommonRuleLinks.Add(new VariableRuleLink<CommonRule>()
                {
                    Variable = this,
                    Entity = entity as CommonRule
                });
            }
            else if (entity is Sign)
            {
                SignLinks.Add(new VariableModRuleLink<Sign>()
                {
                    Variable = this,
                    Entity = entity as Sign,
                    DefinitionType = definitionType
                });
            }
            else if (entity is MenologyRule)
            {
                MenologyRuleLinks.Add(new VariableModRuleLink<MenologyRule>()
                {
                    Variable = this,
                    Entity = entity as MenologyRule,
                    DefinitionType = definitionType
                });
            }
            else if (entity is TriodionRule)
            {
                TriodionRuleLinks.Add(new VariableModRuleLink<TriodionRule>()
                {
                    Variable = this,
                    Entity = entity as TriodionRule,
                    DefinitionType = definitionType
                });
            }
            else if (entity is ExplicitAddRule)
            {
                ExplicitAddRuleLinks.Add(new VariableRuleLink<ExplicitAddRule>()
                {
                    Variable = this,
                    Entity = entity as ExplicitAddRule
                });
            }
        }

        public void ClearLinks<T>(T entity, DefinitionType definitionType = DefinitionType.Rule) where T : RuleEntity//, new()
        {
            if (entity is CommonRule)
            {
                CommonRuleLinks.RemoveAll(c => c.Entity == entity);
            }
            else if (entity is Sign)
            {
                SignLinks.RemoveAll(c => c.Entity == entity && c.DefinitionType == definitionType);
            }
            else if (entity is MenologyRule)
            {
                MenologyRuleLinks.RemoveAll(c => c.Entity == entity && c.DefinitionType == definitionType);
            }
            else if (entity is TriodionRule)
            {
                TriodionRuleLinks.RemoveAll(c => c.Entity == entity && c.DefinitionType == definitionType);
            }
            else if (entity is ExplicitAddRule)
            {
                ExplicitAddRuleLinks.RemoveAll(c => c.Entity == entity);
            }
        }

        /// <summary>
        /// Возвращает Переменную, как она должна выглядеть в определении Правила
        /// </summary>
        /// <returns></returns>
        public string GetShortCode() => $"[{Name}]";
    }
}
