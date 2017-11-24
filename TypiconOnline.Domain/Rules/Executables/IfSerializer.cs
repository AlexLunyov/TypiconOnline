using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class IfSerializer : RuleXmlSerializerBase, IRuleSerializer<If>
    {
        public IfSerializer(IRuleSerializerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] { RuleConstants.IfNodeName };
        }

        public RuleElement Deserialize(IDescriptor descriptor)
        {
            throw new NotImplementedException();
        }

        public string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
