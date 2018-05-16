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
    public class TheotokionRuleSerializer : YmnosRuleSerializer, IRuleSerializer<TheotokionRule>
    {
        public TheotokionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.YmnosRuleTheotokionNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new TheotokionRule(req.Descriptor.GetElementName(), SerializerRoot.BookStorage.TheotokionApp, SerializerRoot.BookStorage.WeekDayApp);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            if (req.Descriptor.Element.SelectSingleNode(RuleConstants.YmnosRuleNode) is XmlNode ymnosNode)
            {
                (req.Element as TheotokionRule).ReferenceYmnos = SerializerRoot.Container<YmnosRule>()
                    .Deserialize(new XmlDescriptor() { Element = ymnosNode }, req.Parent);
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
