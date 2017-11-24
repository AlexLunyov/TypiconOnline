using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class AinoiRuleSerializer : KekragariaRuleSerializer
    {
        public AinoiRuleSerializer(IRuleSerializerRoot unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] {
                RuleConstants.AinoiNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new AinoiRule(d.GetElementName());
        }
    }
}
