using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ViewModels.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KontakionRuleSerializer : KanonasItemRuleBaseSerializer, IRuleSerializer<KontakionRule>
    {
        public KontakionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KKontakionNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new KontakionRule(d.GetElementName(), new KontakionRuleVMFactory(SerializerRoot));

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            base.FillObject(d, element);

            XmlAttribute attr = d.Element.Attributes[RuleConstants.KontakionShowIkosAttrName];
            if (bool.TryParse(attr?.Value, out bool val))
            {
                (element as KontakionRule).ShowIkos = val;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
