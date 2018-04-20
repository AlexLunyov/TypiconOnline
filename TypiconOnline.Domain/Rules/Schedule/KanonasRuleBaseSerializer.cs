using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public abstract class KanonasRuleBaseSerializer : YmnosStructureRuleSerializer
    {
        public KanonasRuleBaseSerializer(IRuleSerializerRoot root) : base(root) { }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(d, container);

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.IsOrthrosAttribute];

            if (bool.TryParse(attr?.Value, out bool isOrthros))
            {
                (container as KanonasRuleBase).IsOrthros = isOrthros;
            }
        }
    }
}
