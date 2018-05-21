﻿using System;
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
        private readonly ITypiconSerializer typiconSerializer = new TypiconSerializer();

        public KontakionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KontakionRuleNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req) 
            => new KontakionRule(req.Descriptor.GetElementName(), typiconSerializer, SerializerRoot.BookStorage.WeekDayApp, new KontakionRuleVMFactory(SerializerRoot));

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.KontakionPlaceAttrName];
            if (Enum.TryParse(attr?.Value, true, out KontakionPlace place))
            {
                (req.Element as KontakionRule).Place = place;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.KontakionShowIkosAttrName];
            if (bool.TryParse(attr?.Value, out bool val))
            {
                (req.Element as KontakionRule).ShowIkos = val;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
