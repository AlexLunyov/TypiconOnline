using System.Collections.Generic;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Versioned.Typicon.Variable;

namespace TypiconOnline.Domain.Versioned.Typicon
{
    public class SignVersion: VersionModRuleBase<Sign, SignVersion>
    {
        public virtual int? TemplateId { get; set; }
        public virtual Sign Template { get; set; }

        public int Priority { get; set; }
        public virtual bool IsAddition { get; set; }

        public virtual ItemText Name { get; set; }

        /// <summary>
        /// Список на используемые в данном Правиле Переменные Устава
        /// </summary>
        //public virtual List<VariableModRuleLink<SignVersion>> VariableLinks { get; set; }
    }
}
