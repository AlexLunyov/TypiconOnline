using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Serialization
{
    public abstract class RuleXmlSerializerBase
    {
        protected IRuleSerializerUnitOfWork _unitOfWork;

        public RuleXmlSerializerBase(IRuleSerializerUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("RuleFactoryUnitOfWork");
        }

        public IEnumerable<string> ElementNames { get; protected set; }
    }
}
