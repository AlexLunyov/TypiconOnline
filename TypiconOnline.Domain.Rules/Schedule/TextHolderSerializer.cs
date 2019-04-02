using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Output.Factories;

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

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new TextHolder(new TextHolderVMFactory(SerializerRoot), req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            var obj = req.Element as TextHolder;

            if (Enum.TryParse(req.Descriptor.Element.Name, true, out TextHolderKind kind))
            {
                obj.Kind = kind;
            }

            var attr = req.Descriptor.Element.Attributes?[RuleConstants.TextHolderMarkAttr];
            if (Enum.TryParse(attr?.Value, true, out TextHolderMark mark))
            {
                obj.Mark = mark;
            }

            foreach (XmlNode childNode in req.Descriptor.Element.ChildNodes)
            {
                if (childNode.Name == RuleConstants.TextHolderPapragraphNode)
                {
                    var item = SerializerRoot.TypiconSerializer.Deserialize<ItemTextNoted>(childNode.OuterXml, RuleConstants.TextHolderPapragraphNode);
                    //new ItemTextNoted(childNode.OuterXml, RuleConstants.TextHolderPapragraphNode);

                    obj.Paragraphs.Add(item);
                }
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
