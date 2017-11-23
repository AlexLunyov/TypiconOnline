using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Factories;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleFactoryUnitOfWork
    {
        RuleBaseFactoryContainer<T> Factory<T>() where T : RuleElement;
    }
}
