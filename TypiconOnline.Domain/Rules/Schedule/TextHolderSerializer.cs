using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class TextHolderSerializer : RuleXmlSerializerBase, IRuleSerializer<TextHolder>
    {
        public TextHolderSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.TextHolderLectorNode,
                RuleConstants.TextHolderChoirNode,
                RuleConstants.TextHolderDeaconNode,
                RuleConstants.TextHolderPriestNode,
                RuleConstants.TextHolderTextNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new TextHolder(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            if (Enum.TryParse(d.Element.Name, true, out TextHolderKind kind))
            {
                (element as TextHolder).Kind = kind;
            }

            XmlAttribute attr = d.Element.Attributes[RuleConstants.TextHolderMarkAttr];
            if (Enum.TryParse(attr?.Value, true, out TextHolderMark mark))
            {
                (element as TextHolder).Mark = mark;
            }

            foreach (XmlNode childNode in d.Element.ChildNodes)
            {
                ItemTextNoted item = new ItemTextNoted((childNode.Name == RuleConstants.TextHolderPapragraphNode) ? childNode.OuterXml : string.Empty);

                (element as TextHolder).Paragraphs.Add(item);
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
