using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class TheotokionRuleSerializer : YmnosRuleSerializer, IRuleSerializer<TheotokionRule>
    {
        public TheotokionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.YmnosRuleTheotokionNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new TheotokionRule(d.GetElementName(), SerializerRoot.BookStorage.TheotokionApp);
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            base.FillObject(d, element);

            if (d.Element.SelectSingleNode(RuleConstants.YmnosRuleNode) is XmlNode ymnosNode)
            {
                (element as TheotokionRule).ReferenceYmnos = SerializerRoot.Container<YmnosRule>()
                    .Deserialize(new XmlDescriptor() { Element = ymnosNode });
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
