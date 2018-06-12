using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ViewModels.Factories;

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

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new TextHolder(new TextHolderVMFactory(SerializerRoot), req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            if (Enum.TryParse(req.Descriptor.Element.Name, true, out TextHolderKind kind))
            {
                (req.Element as TextHolder).Kind = kind;
            }

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.TextHolderMarkAttr];
            if (Enum.TryParse(attr?.Value, true, out TextHolderMark mark))
            {
                (req.Element as TextHolder).Mark = mark;
            }

            foreach (XmlNode childNode in req.Descriptor.Element.ChildNodes)
            {
                if (childNode.Name == RuleConstants.TextHolderPapragraphNode)
                {
                    ItemTextNoted item = new ItemTextNoted(childNode.OuterXml, RuleConstants.TextHolderPapragraphNode);

                    (req.Element as TextHolder).Paragraphs.Add(item);
                }
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
