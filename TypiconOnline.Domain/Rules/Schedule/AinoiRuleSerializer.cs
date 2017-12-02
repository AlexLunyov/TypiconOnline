using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ViewModels.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class AinoiRuleSerializer : KekragariaRuleSerializer, IRuleSerializer<AinoiRule>
    {
        public AinoiRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.AinoiNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new AinoiRule(new AinoiRuleVMFactory(SerializerRoot), SerializerRoot, d.GetElementName());
        }
    }
}
