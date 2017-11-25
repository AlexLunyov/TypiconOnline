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
    public class KKanonasItemRuleSerializer : KKontakionRuleSerializer, IRuleSerializer<KKanonasItemRule>
    {
        public KKanonasItemRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KanonasItemNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new KKanonasItemRule(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            base.FillObject(d, element);

            XmlAttribute attr = d.Element.Attributes[RuleConstants.KanonasCountAttrName];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                (element as KKanonasItemRule).Count = intValue;
            }

            attr = d.Element.Attributes[RuleConstants.KanonasMartyrionAttrName];
            (element as KKanonasItemRule).UseMartyrion = bool.TryParse(attr?.Value, out bool value) ? value : false;

            attr = d.Element.Attributes[RuleConstants.KanonasIrmosCountAttrName];
            (element as KKanonasItemRule).IrmosCount = int.TryParse(attr?.Value, out int i) ? i : 0;
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
