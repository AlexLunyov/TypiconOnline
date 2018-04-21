using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule.Extensions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KAfterRuleSerializer : ExecContainerSerializer, IRuleSerializer<KAfterRule>
    {
        public KAfterRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KAfterNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req) => new KAfterRule(req.Descriptor.GetElementName());

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            var attr = req.Descriptor.Element.Attributes[RuleConstants.KAfterOdiNumberAttrName];

            if (int.TryParse(attr?.Value, out int intValue))
            {
                (req.Element as KAfterRule).OdiNumber = intValue;
            }

            (req.Element as KAfterRule).FillElement(req.Descriptor.Element);
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
