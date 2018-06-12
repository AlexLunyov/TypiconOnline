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
    public class IntSerializer : RuleXmlSerializerBase, IRuleSerializer<Int>
    {
        public IntSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IntNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new Int(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            (req.Element as Int).ValueExpression = new ItemInt(req.Descriptor.Element.InnerText);
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
