using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KOdiRuleSerializer : ExecContainerSerializer, IRuleSerializer<KOdiRule>
    {
        public KOdiRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KOdiRuleNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req) 
            => new KOdiRule(req.Descriptor.GetElementName(), req.Parent as KanonasRule);

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            var attr = req.Descriptor.Element.Attributes[RuleConstants.KOdiRuleNumberAttrName];

            if (int.TryParse(attr?.Value, out int intValue))
            {
                (req.Element as KOdiRule).Number = intValue;
            }

            (req.Element as KOdiRule).FillElement(req.Descriptor.Element);
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
