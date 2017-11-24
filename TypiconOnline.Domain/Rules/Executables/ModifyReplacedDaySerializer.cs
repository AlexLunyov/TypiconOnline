using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ModifyReplacedDaySerializer : ModifyDaySerializer, IRuleSerializer<ModifyReplacedDay>
    {
        public ModifyReplacedDaySerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.ModifyReplacedDayNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new ModifyReplacedDay(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            base.FillObject(d, element);

            XmlAttribute attr = d.Element.Attributes[RuleConstants.KindAttrName];

            if (Enum.TryParse(attr?.Value, true, out KindOfReplacedDay value))
            {
                (element as ModifyReplacedDay).Kind = value;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
