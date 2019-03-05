using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Extensions
{
    public static class IAsAdditionElementXmlSerialization
    {
        public static void FillIAsAdditionElement(this IAsAdditionElement element, XmlNode node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.IAsAdditionElementModeAttrName];
            element.AsAdditionMode = (Enum.TryParse(attr?.Value, true, out AsAdditionMode mode)) ? mode : AsAdditionMode.None;
        }
    }
}
