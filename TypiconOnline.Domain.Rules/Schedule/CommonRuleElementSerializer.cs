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

        protected override RuleElement CreateObject(CreateObjectRequest req) => new CommonRuleElement(SerializerRoot);

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            (req.Element as CommonRuleElement).CommonRuleName = req.Descriptor.Element.Attributes[RuleConstants.CommonRuleNameAttr]?.Value;
        }
    }
}
