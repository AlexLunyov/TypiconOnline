using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ModifyReplacedDaySerializer : ModifyDaySerializer, IRuleSerializer<ModifyReplacedDay>
    {
        public ModifyReplacedDaySerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.ModifyReplacedDayNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req) 
            => new ModifyReplacedDay(req.Descriptor.GetElementName(), req.Parent);

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.KindAttrName];

            if (Enum.TryParse(attr?.Value, true, out KindOfReplacedDay value))
            {
                (req.Element as ModifyReplacedDay).Kind = value;
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
