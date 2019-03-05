using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KAfterRuleSerializer : ExecContainerSerializer, IRuleSerializer<KAfterRule>
    {
        public KAfterRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KAfterNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req) 
            => new KAfterRule(req.Descriptor.GetElementName(), req.Parent as KanonasRule);

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            var attr = req.Descriptor.Element.Attributes[RuleConstants.KAfterOdiNumberAttrName];

            if (int.TryParse(attr?.Value, out int intValue))
            {
                (req.Element as KAfterRule).OdiNumber = intValue;
            }

            //IAsAdditionElement
            (req.Element as KAfterRule).FillIAsAdditionElement(req.Descriptor.Element);
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
