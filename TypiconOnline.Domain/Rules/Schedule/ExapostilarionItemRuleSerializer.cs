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
    public class ExapostilarionItemRuleSerializer : SourceHavingRuleBaseSerializer, IRuleSerializer<ExapostilarionItemRule>
    {
        public ExapostilarionItemRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.ExapostilarionItemRuleNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req) 
            => new ExapostilarionItemRule(req.Descriptor.GetElementName(), typiconSerializer, SerializerRoot.QueryProcessor);

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            ExapostilarionItemRule obj = req.Element as ExapostilarionItemRule;

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.ExapostilarionItemRulePlaceAttrName];
            if (Enum.TryParse(attr?.Value, true, out ExapostilarionItemPlace place))
            {
                obj.Place = place;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.ExapostilarionItemRuleKindAttrName];
            if (Enum.TryParse(attr?.Value, true, out ExapostilarionItemKind kind))
            {
                obj.Kind = kind;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.ExapostilarionItemRuleCountAttrName];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                obj.Count = intValue;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
