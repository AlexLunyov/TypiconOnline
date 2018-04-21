using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule.Extensions
{
    public static class IAsAdditionElementXmlSerialization
    {
        public static void FillElement(this IAsAdditionElement element, XmlNode node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.IAsAdditionElementModeAttrName];
            element.AsAdditionMode = (Enum.TryParse(attr?.Value, true, out AsAdditionMode mode)) ? mode : AsAdditionMode.None;
        }
    }
}
