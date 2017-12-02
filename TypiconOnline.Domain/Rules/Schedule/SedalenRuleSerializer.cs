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
    public class SedalenRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<SedalenRule>
    {
        public SedalenRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.SedalenNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new SedalenRule(new SedalenVMFactory(SerializerRoot), SerializerRoot, d.GetElementName());
        }
    }
}
