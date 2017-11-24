using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class YmnosRuleSerializer : RuleXmlSerializerBase, IRuleSerializer<YmnosRule>
    {
        public YmnosRuleSerializer(IRuleSerializerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] {
                RuleConstants.YmnosRuleNode,
                RuleConstants.YmnosRuleDoxastichonNode };
        }

        public RuleElement Deserialize(IDescriptor descriptor)
        {
            throw new NotImplementedException();
        }

        public string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
