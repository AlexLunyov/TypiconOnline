﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class DaysFromEasterSerializer : RuleXmlSerializerBase, IRuleSerializer<DaysFromEaster>
    {
        public DaysFromEasterSerializer(IRuleSerializerRoot unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] { RuleConstants.DaysFromEasterNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new DaysFromEaster(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            if (d.Element.HasChildNodes)
            {
                (element as DaysFromEaster).ChildExpression = _unitOfWork.Factory<DateExpression>()
                    .CreateElement(new XmlDescriptor() { Element = d.Element.FirstChild });
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
