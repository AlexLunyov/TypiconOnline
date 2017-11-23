using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Factories
{
    public abstract class RuleXmlFactoryBase
    {
        protected IRuleFactoryUnitOfWork _unitOfWork;

        public RuleXmlFactoryBase(IRuleFactoryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("RuleFactoryUnitOfWork");
        }

        public IEnumerable<string> ElementNames { get; protected set; }
    }
}
