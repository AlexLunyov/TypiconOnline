using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class RuleHandlerSettings
    {
        public TypiconRule Rule;
        public List<DayService> DayServices;

        public HandlingMode Mode;
        public bool PutSeniorRuleNameToEnd = false;
        public string ShortName;
        public bool UseFullName = true;
        /// <summary>
        /// Язык для обработки
        /// </summary>
        public string Language;

        public RuleHandlerSettings()
        {
            DayServices = new List<DayService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seniorTypiconRule">Главное правило для обработки</param>
        public RuleHandlerSettings(TypiconRule seniorTypiconRule) : this()
        {
            Rule = seniorTypiconRule;
        }
    }
}
