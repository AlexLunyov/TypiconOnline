using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class EktenisRuleSerializer : ExecContainerSerializer, IRuleSerializer<EktenisRule>
    {
        public EktenisRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.EktenisNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new EktenisRule(d.GetElementName());
    }
}
