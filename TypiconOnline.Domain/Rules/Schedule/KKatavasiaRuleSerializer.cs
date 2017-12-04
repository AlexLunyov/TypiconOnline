using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KKatavasiaRuleSerializer : KanonasItemRuleBaseSerializer, IRuleSerializer<KKatavasiaRule>
    {
        public KKatavasiaRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KKatavasiaNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new KKatavasiaRule(d.GetElementName(), SerializerRoot.BookStorage.Katavasia);
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            base.FillObject(d, element);

            (element as KKatavasiaRule).Name = d.Element.Attributes[RuleConstants.KKatavasiaNameAttr]?.Value;
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
