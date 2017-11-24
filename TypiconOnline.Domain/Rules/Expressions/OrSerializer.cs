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
    public class OrSerializer : LogicalExpressionSerializer, IRuleSerializer<Or>
    {
        public OrSerializer(IRuleSerializerRoot unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] { RuleConstants.OrNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new Or(d.GetElementName());
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
