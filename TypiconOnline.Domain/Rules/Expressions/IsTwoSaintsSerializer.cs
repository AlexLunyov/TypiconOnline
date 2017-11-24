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
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class IsTwoSaintsSerializer : RuleXmlSerializerBase, IRuleSerializer<IsTwoSaints>
    {
        public IsTwoSaintsSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IsTwoSaintsNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new IsTwoSaints(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
