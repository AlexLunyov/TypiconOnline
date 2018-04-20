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
        public YmnosStructureRuleSerializer(IRuleSerializerRoot root) : base(root) { }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            if (Enum.TryParse(req.Descriptor.GetElementName(), true, out YmnosStructureKind kind))
            {
                (req.Element as YmnosStructureRule).Kind = kind;
            }

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.TotalCountAttribute];

            if (int.TryParse(attr?.Value, out int count))
            {
                (req.Element as YmnosStructureRule).TotalYmnosCount = count;
            }
        }
    }
}
