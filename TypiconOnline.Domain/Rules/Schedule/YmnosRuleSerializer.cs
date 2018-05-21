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
            => new YmnosRule(req.Descriptor.GetElementName(), typiconSerializer, SerializerRoot.BookStorage.WeekDayApp);

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.YmnosRulePlaceAttrName];
            if (Enum.TryParse(attr?.Value, true, out PlaceYmnosSource place))
            {
                (req.Element as YmnosRule).Place = place;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.YmnosRuleCountAttrName];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                (req.Element as YmnosRule).Count = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.YmnosRuleStartFromAttrName];
            if (int.TryParse(attr?.Value, out intValue))
            {
                (req.Element as YmnosRule).StartFrom = intValue;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
