using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KAfterRuleSerializer : ExecContainerSerializer, IRuleSerializer<KAfterRule>
    {
        public KAfterRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KAfterNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new KAfterRule(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            base.FillObject(d, element);

            var attr = d.Element.Attributes[RuleConstants.KAfterOdiNumberAttrName];

            if (int.TryParse(attr?.Value, out int intValue))
            {
                (element as KAfterRule).OdiNumber = intValue;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
