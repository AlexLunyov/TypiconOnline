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
    public class IsCelebratingSerializer : RuleXmlSerializerBase, IRuleSerializer<IsCelebrating>
    {
        public IsCelebratingSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IsCelebratingNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new IsCelebrating(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }

        
    }
}
