using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ViewModels.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class PsalmRuleSerializer : RuleXmlSerializerBase, IRuleSerializer<PsalmRule>
    {
        public PsalmRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.PsalmRuleNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req) 
            => new PsalmRule(req.Descriptor.GetElementName(), SerializerRoot.QueryProcessor, new PsalmRuleVMFactory(SerializerRoot));

        protected override void FillObject(FillObjectRequest req)
        {
            var psalmRule = req.Element as PsalmRule;
            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.PsalmRuleNumberAttr];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                psalmRule.Number = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.PsalmRuleStartAttr];
            if (int.TryParse(attr?.Value, out intValue))
            {
                psalmRule.StartStihos = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.PsalmRuleEndAttr];
            if (int.TryParse(attr?.Value, out intValue))
            {
                psalmRule.EndStihos = intValue;
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
