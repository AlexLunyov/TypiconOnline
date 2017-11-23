using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Factories;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class IfFactory : RuleXmlFactoryBase, IRuleFactory<RuleExecutable>
    {
        public IfFactory(IRuleFactoryUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public RuleExecutable Create(IDescriptor descriptor)
        {
            throw new NotImplementedException();
        }
    }
}
