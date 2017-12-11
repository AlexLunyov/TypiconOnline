using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class CommonRuleElementSerializer : ExecContainerSerializer, IRuleSerializer<CommonRuleElement>
    {
        public CommonRuleElementSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.CommonRuleNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new CommonRuleElement(SerializerRoot);

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            base.FillObject(d, element);

            (element as CommonRuleElement).CommonRuleName = d.Element.Attributes[RuleConstants.CommonRuleNameAttr]?.Value;
        }
    }
}
