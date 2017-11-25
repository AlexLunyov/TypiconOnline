using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IModifiedRuleService
    {
        ICollection<ModifiedRule> GetModifiedRules(TypiconEntity typicon, DateTime date);
    }
}
