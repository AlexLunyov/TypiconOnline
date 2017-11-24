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
    /// Правило для стихир на стиховне
    /// </summary>
    public class ApostichaRule : YmnosStructureRule
    {
        public ApostichaRule(string name) : base(name) { }
        public ApostichaRule(XmlNode node) : base(node) { }

        public override ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new ApostichaViewModel(this, handler);
        }

    }
}
