using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.Command.Typicon
{
    public abstract class TypiconByRuleCommandBase<T> : ICommand, IHasAuthorizedAccess where T : class, ITypiconVersionChild, new()
    {
        public TypiconByRuleCommandBase(int ruleId)
        {
            RuleId = ruleId;
            Key = new TypiconByRuleCanEditKey<T>(ruleId);
        }

        public int RuleId { get; }

        public IAuthorizeKey Key { get; }
    }
}
