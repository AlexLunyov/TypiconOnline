using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ViewModels.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KontakionRuleSerializer : SourceHavingRuleBaseSerializer, IRuleSerializer<KontakionRule>
    {
        public KontakionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KontakionRuleNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req) 
            => new KontakionRule(req.Descriptor.GetElementName(), typiconSerializer, SerializerRoot.QueryProcessor, new KontakionRuleVMFactory(SerializerRoot));

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            KontakionRule obj = req.Element as KontakionRule;

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.KontakionPlaceAttrName];
            if (Enum.TryParse(attr?.Value, true, out KontakionPlace place))
            {
                obj.Place = place;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.YmnosRuleKindAttrName];
            if (Enum.TryParse(attr?.Value, true, out YmnosRuleKind kind))
            {
                obj.Kind = kind;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.KontakionShowIkosAttrName];
            if (bool.TryParse(attr?.Value, out bool val))
            {
                obj.ShowIkos = val;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
