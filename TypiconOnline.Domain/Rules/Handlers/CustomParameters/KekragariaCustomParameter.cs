using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    /// <summary>
    /// кастомная настройка для класса KekragariaRule
    /// </summary>
    public class KekragariaCustomParameter : CustomParameterBase<KekragariaRule>
    {
        public bool? ShowPsalm { get; set; }

        public int? TotalYmnosCount { get; set; }

        protected override void InnerApply(KekragariaRule element)
        {
            if (ShowPsalm != null)
            {
                element.ShowPsalm = (bool) ShowPsalm;
            }

            if (TotalYmnosCount != null)
            {
                element.TotalYmnosCount.Value = (int) TotalYmnosCount;
            }
        }
    }
}
