using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetRuleSettingsRequest
    {
        public MenologyRule MenologyRule { get; set; }
        public TriodionRule TriodionRule { get; set; }
        public OktoikhDay OktoikhDay { get; set; }
        public ModifiedRule ModifiedRule { get; set; }
        //public TypiconEntity Typicon { get; set; }
        public DateTime Date { get; set; }
        public string Language { get; set; }
        public HandlingMode Mode { get; set; }
        public IEnumerable<IScheduleCustomParameter> CustomParameters { get; set; }
    }
}
