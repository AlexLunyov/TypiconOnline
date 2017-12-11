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
    public class TroparionRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<TroparionRule>
    {
        public TroparionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.TroparionNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new TroparionRule(new TroparionRuleVMFactory(SerializerRoot), SerializerRoot, d.GetElementName());
        }
    }
}