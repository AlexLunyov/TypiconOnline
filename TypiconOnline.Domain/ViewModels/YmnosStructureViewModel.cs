using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels
{
    public class YmnosStructureViewModel : ContainerViewModel
    {
        public YmnosStructureKind Kind { get; set; }

        public int Ihos { get; set; }
        /// <summary>
        /// "Глас" текст
        /// </summary>
        public string IhosText { get; set; }

        public YmnosStructureViewModel(YmnosStructureRule rule, IRuleHandler handler) : base()
        {
            if (rule == null || rule.CalculatedYmnosStructure == null) throw new ArgumentNullException("ymnosStructure");
            if (handler == null) throw new ArgumentNullException("handler");

            rule.ThrowExceptionIfInvalid();

            Kind = rule.YmnosStructureKind.Value;

            //groups
            for (int i = 0; i < rule.CalculatedYmnosStructure.Groups.Count; i++)
            {
                YmnosGroup group = rule.CalculatedYmnosStructure.Groups[i];

                YmnosGroupViewModel item = new YmnosGroupViewModel(group, handler);

                if (i == 0)
                {
                    Ihos = group.Ihos;
                    IhosText = item.IhosText;
                }

                ChildElements.Add(item);
            }

            if (rule.CalculatedYmnosStructure.Doxastichon != null)
            {
                ChildElements.Add(new YmnosGroupViewModel(rule.CalculatedYmnosStructure.Doxastichon, handler));
            }

            if (rule.CalculatedYmnosStructure.Theotokion?.Count > 0)
            {
                ChildElements.Add(new YmnosGroupViewModel(rule.CalculatedYmnosStructure.Theotokion[0], handler));
            }
        }
    }
}
