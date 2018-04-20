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
    public class KKatavasiaRuleSerializer : KanonasItemRuleBaseSerializer, IRuleSerializer<KKatavasiaRule>
    {
        public KKatavasiaRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KKatavasiaNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new KKatavasiaRule(req.Descriptor.GetElementName(), SerializerRoot.BookStorage.Katavasia);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            (req.Element as KKatavasiaRule).Name = req.Descriptor.Element.Attributes[RuleConstants.KKatavasiaNameAttr]?.Value;
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
