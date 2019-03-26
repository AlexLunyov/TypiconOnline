using System;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public abstract class IntExpression : RuleExpression<int>
    {
        public IntExpression(string name) : base(name) { }
    }
}
