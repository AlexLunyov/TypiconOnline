using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Typicon
{
    public abstract class TypiconRule : RuleEntity//, IRuleHandlerInitiator
    {
        public virtual Sign Template { get; set; }

        public virtual TypiconEntity Owner { get; set; }
        /// <summary>
        /// Возвращает Правило: либо свое, либо шаблонное
        /// </summary>
        public override RuleContainer Rule
        {
            get
            {
                if ((base.Rule == null) && string.IsNullOrEmpty(RuleDefinition))
                {
                    return Template.Rule;
                }

                return base.Rule;//(string.IsNullOrEmpty(RuleDefinition)) ? Template.Rule : null;
            }
        }
    }
}
