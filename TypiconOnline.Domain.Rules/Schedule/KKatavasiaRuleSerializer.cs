﻿using System;
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

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new KKatavasiaRule(req.Descriptor.GetElementName(), SerializerRoot.QueryProcessor, SerializerRoot.TypiconSerializer);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            (req.Element as KKatavasiaRule).Name = req.Descriptor.Element.Attributes[RuleConstants.KKatavasiaNameAttr]?.Value;
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
