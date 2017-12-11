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
    public class KekragariaRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<KekragariaRule>
    {
        public KekragariaRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.KekragariaRuleNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new KekragariaRule(new KekragariaRuleVMFactory(SerializerRoot), SerializerRoot, d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement container)
        {
            base.FillObject(d, container);

            XmlAttribute attr = d.Element.Attributes[RuleConstants.ShowPsalmAttribute];

            if (bool.TryParse(attr?.Value, out bool showPsalm))
            {
                (container as KekragariaRule).ShowPsalm = showPsalm;
            }
        }
    }
}
