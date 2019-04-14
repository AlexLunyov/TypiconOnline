using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Extensions
{
    public static class SerializationExtensions
    {
        public static List<RuleElementBase> DeserializeChildren(this XmlNodeList list, IRuleSerializerRoot root, IAsAdditionElement parent)
        {
            var result = new List<RuleElementBase>();

            foreach (XmlNode childNode in list)
            {
                var child = root.Container<RuleElementBase>().Deserialize(new XmlDescriptor() { Element = childNode }, parent);
                result.Add(child);
            }

            return result;
        }
    }
}
