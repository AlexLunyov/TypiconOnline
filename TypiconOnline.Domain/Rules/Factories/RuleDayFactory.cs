using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Rules.Factories
{
    public static class RuleDayFactory
    {
        public static DayContainer CreateDayElement(XmlNode node)
        {
            DayContainer outputEl = null;
            switch (node.Name)
            {
                case RuleConstants.DayElementNode:
                    outputEl = new DayContainer(node);
                    break;
            }
            return outputEl;
        }
    }
}
