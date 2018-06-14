using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Tests.Common
{
    public class IsAdditionTestHandler : RuleHandlerBase<IReadOnlyList<RuleElement>>
    {
        List<RuleElement> container = new List<RuleElement>();

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
            container.Add(element as RuleElement);

            return true;
        }

        public override IReadOnlyList<RuleElement> GetResult() => container;
    }
}
