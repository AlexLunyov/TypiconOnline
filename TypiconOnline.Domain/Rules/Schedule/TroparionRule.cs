using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для стихир на стиховне и литии
    /// </summary>
    public class TroparionRule : YmnosStructureRule
    {
        public TroparionRule(string name) : base(name) { }

        public override ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new ApostichaViewModel(this, handler);
        }

    }
}
