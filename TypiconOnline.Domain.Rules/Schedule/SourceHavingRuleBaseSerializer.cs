using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public abstract class SourceHavingRuleBaseSerializer : RuleXmlSerializerBase
    {
        protected readonly ITypiconSerializer typiconSerializer = new TypiconSerializer();

        public SourceHavingRuleBaseSerializer(IRuleSerializerRoot root) : base(root) { }

        protected override void FillObject(FillObjectRequest req)
        {
            var obj = req.Element as SourceHavingRuleBase;

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.YmnosRuleSourceAttrName];
            if (Enum.TryParse(attr?.Value, true, out YmnosSource source))
            {
                obj.Source = source;
            }
        }
    }
}
