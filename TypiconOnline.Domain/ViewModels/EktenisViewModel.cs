using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels
{
    public class EktenisViewModel : ContainerViewModel
    {
        public EktenisViewModel(EktenisRule rule, IRuleHandler handler) : base()
        {
            if (rule == null || rule.CalculatedElements == null) throw new ArgumentNullException("Ektenis");
            if (handler == null) throw new ArgumentNullException("handler");

            rule.ThrowExceptionIfInvalid();

            //Text = rule.Name[handler.Settings.Language];

            foreach (TextHolder text in rule.CalculatedElements)
            {
                ChildElements.Add( new TextHolderViewModel(text, handler) );
            }
        }
    }
}
