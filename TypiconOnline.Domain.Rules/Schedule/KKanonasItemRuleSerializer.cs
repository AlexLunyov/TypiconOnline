﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KKanonasItemRuleSerializer : KanonasItemRuleBaseSerializer, IRuleSerializer<KKanonasItemRule>
    {
        public KKanonasItemRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KanonasItemNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req) => new KKanonasItemRule(req.Descriptor.GetElementName());

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.KanonasCountAttrName];
            if (int.TryParse(attr?.Value, out int intValue))
            {
                (req.Element as KKanonasItemRule).Count = intValue;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.KanonasMartyrionAttrName];
            (req.Element as KKanonasItemRule).UseMartyrion = bool.TryParse(attr?.Value, out bool value) ? value : true;

            attr = req.Descriptor.Element.Attributes[RuleConstants.KanonasIrmosCountAttrName];
            (req.Element as KKanonasItemRule).IrmosCount = int.TryParse(attr?.Value, out int i) ? i : 0;
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
