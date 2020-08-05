using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Versioned.Typicon
{
    public abstract class VersionRuleBase<T, U> : VersionLinkedBase<T, U> where T : EntityBase, IHasId<int>, new()
                                                                where U : VersionBase, new()
    {

        public string RuleDefinition { get; set; }
    }
}
