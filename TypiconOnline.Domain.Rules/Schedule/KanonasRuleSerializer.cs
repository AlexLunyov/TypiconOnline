﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.ViewModels.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KanonasRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<KanonasRule>
    {
        public KanonasRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.KanonasRuleNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new KanonasRule(req.Descriptor.GetElementName(), new KanonasRuleVMFactory(SerializerRoot), req.Parent);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.IsOrthrosAttribute];

            if (bool.TryParse(attr?.Value, out bool isOrthros))
            {
                (req.Element as KanonasRule).IsOrthros = isOrthros;
            }

            (req.Element as IAsAdditionElement).FillElement(req.Descriptor.Element);
        }
    }
}
