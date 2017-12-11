using System;
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
    public class LessSerializer : LogicalExpressionSerializer, IRuleSerializer<Less>
    {
        public LessSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.LessNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new Less(d.GetElementName());
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
