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
    public abstract class YmnosStructureRuleSerializer : ExecContainerSerializer
    {
        public YmnosStructureRuleSerializer(IRuleSerializerUnitOfWork unitOfWork) : base(unitOfWork) { }

        protected override void FillObject(XmlDescriptor d, ExecContainer container)
        {
            base.FillObject(d, container);

            if (Enum.TryParse(d.GetElementName(), true, out YmnosStructureKind kind))
            {
                (container as YmnosStructureRule).Kind = kind;
            }

            XmlAttribute attr = d.Element.Attributes[RuleConstants.TotalCountAttribute];

            if (int.TryParse(attr?.Value, out int count))
            {
                (container as YmnosStructureRule).TotalYmnosCount = count;
            }
        }
    }
}
