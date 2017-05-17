using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Rules.Factories
{
    static class RuleContainerFactory
    {
        public static RuleContainer CreateRuleContainer(string description)
        {
            ExecContainer outputEl = null;

            if (string.IsNullOrEmpty(description))
            {
                return null;
            }

            //try
            //{
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(description);
            
                if ((doc != null) && (doc.DocumentElement != null))
                {
                    XmlNode node = doc.DocumentElement;
                
                    switch (node.Name)
                    {
                        case RuleConstants.ExecContainerNodeName:
                        case RuleConstants.ActionNodeName:
                            outputEl = new ExecContainer(node);
                            break;
                            //default:
                            //    throw new DefinitionsParsingException("Ошибка: несанкционированный элемент " + node.Name);
                    }
                }
            //}
            //catch (Exception ex)
            //{

            //}

            return outputEl;
        }
    }
}
