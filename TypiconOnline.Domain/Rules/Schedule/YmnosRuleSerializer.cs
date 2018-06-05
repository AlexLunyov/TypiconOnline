using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class YmnosRuleSerializer : SourceHavingRuleBaseSerializer, IRuleSerializer<YmnosRule>
    {
        public YmnosRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.YmnosRuleNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req) 
            => new YmnosRule(req.Descriptor.GetElementName(), typiconSerializer, SerializerRoot.QueryProcessor);

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            YmnosRule obj = req.Element as YmnosRule;

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.YmnosRulePlaceAttrName];
            if (Enum.TryParse(attr?.Value, true, out PlaceYmnosSource place))
            {
                obj.Place = place;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.YmnosRuleKindAttrName];
            if (Enum.TryParse(attr?.Value, true, out YmnosRuleKind kind))
            {
                obj.Kind = kind;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.YmnosRuleCountAttrName];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                obj.Count = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.YmnosRuleStartFromAttrName];
            if (int.TryParse(attr?.Value, out intValue))
            {
                obj.StartFrom = intValue;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
