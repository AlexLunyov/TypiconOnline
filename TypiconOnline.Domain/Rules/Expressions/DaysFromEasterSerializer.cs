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
    public class DaysFromEasterSerializer : RuleXmlSerializerBase, IRuleSerializer<DaysFromEaster>
    {
        public DaysFromEasterSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.DaysFromEasterNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new DaysFromEaster(req.Descriptor.GetElementName(), SerializerRoot.BookStorage.Easters);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            if (req.Descriptor.Element.HasChildNodes)
            {
                (req.Element as DaysFromEaster).ChildExpression = SerializerRoot.Container<DateExpression>()
                    .Deserialize(new XmlDescriptor() { Element = req.Descriptor.Element.FirstChild }, req.Parent);
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
