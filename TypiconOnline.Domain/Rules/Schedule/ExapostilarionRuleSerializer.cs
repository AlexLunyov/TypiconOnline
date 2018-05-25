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
    public class ExapostilarionRuleSerializer : ExecContainerSerializer, IRuleSerializer<ExapostilarionRule>
    {
        public ExapostilarionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.ExapostilarionRuleNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new ExapostilarionRule(new ExapostilarionRuleVMFactory(SerializerRoot), req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.ShowPsalmAttribute];

            if (bool.TryParse(attr?.Value, out bool showPsalm))
            {
                (req.Element as KekragariaRule).ShowPsalm = showPsalm;
            }
        }
    }
}
