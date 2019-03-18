using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Tests.Common
{
    public class IsAdditionTestHandler : RuleHandlerBase<IReadOnlyList<RuleElementBase>>
    {
        List<RuleElementBase> container = new List<RuleElementBase>();

        public IsAdditionTestHandler()
        {
            AuthorizedTypes = null;

            ResctrictedTypes = new List<Type>()
            {
                typeof(ModifyDay),
                typeof(ModifyReplacedDay)
            };
        }

        public override void ClearResult()
        {
            container.Clear();
        }

        public override bool Execute(ICustomInterpreted element)
        {
            container.Add(element as RuleElementBase);

            return true;
        }

        public override IReadOnlyList<RuleElementBase> GetResult() => container;
    }
}
