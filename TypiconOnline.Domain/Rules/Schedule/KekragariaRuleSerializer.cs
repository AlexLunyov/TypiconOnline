using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KekragariaRuleSerializer : ExecContainerSerializer
    {
        public KekragariaRuleSerializer(IRuleSerializerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] {
                RuleConstants.KekragariaRuleNode };
        }

        protected override ExecContainer CreateObject(XmlDescriptor d)
        {
            return new KekragariaRule(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, ExecContainer container)
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
