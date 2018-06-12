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
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class MoreEqualsSerializer : LogicalExpressionSerializer, IRuleSerializer<MoreEquals>
    {
        public MoreEqualsSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.MoreEqualsNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new MoreEquals(req.Descriptor.GetElementName());
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
