using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Books.Psalter
{
    [Serializable]
    [XmlRoot(RuleConstants.PsalmStihosNode)]
    public class PsalmStihos : ItemText, IPsalterElement
    {
        public int? Number { get; set; }

        public override void ReadXml(XmlReader reader)
        {
            reader.MoveToElement();

            string numberString = reader.GetAttribute(RuleConstants.ReadingStihosNumberAttr);

            if (int.TryParse(numberString, out int i))
            {
                Number = i;
            }

            base.ReadXml(reader);
        }

        public override void WriteXml(XmlWriter writer)
        {
            if (Number != null)
            {
                writer.WriteAttributeString(RuleConstants.ReadingStihosNumberAttr, string.Empty, Number.ToString());
            }
            
            base.WriteXml(writer);
        }
    }
}
