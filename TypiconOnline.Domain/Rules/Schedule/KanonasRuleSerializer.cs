using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ViewModels.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KanonasRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<KanonasRule>
    {
        public KanonasRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.KanonasRuleNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new KanonasRule(d.GetElementName(), SerializerRoot, new KanonasRuleVMFactory(SerializerRoot));
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            base.FillObject(d, element);

            SetValues((element as KanonasRule).Panagias, d.Element.Attributes[RuleConstants.KanonasRulePanagiasAttrName]);

            void SetValues(CommonRuleElement el, XmlAttribute attr)
            {
                el = (attr != null) ? new CommonRuleElement(SerializerRoot) { CommonRuleName = attr.Value } : null;
            }
        }
    }
}
