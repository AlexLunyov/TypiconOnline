using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class ExapostilarionRuleVMFactory : ViewModelFactoryBase<ExapostilarionRule>
    {
        public ExapostilarionRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer)
        {
        }

        public override void Create(CreateViewModelRequest<ExapostilarionRule> req)
        {
            //nothing yet
        }
    }
}
