using System;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public abstract class BooleanExpression : RuleExpression<bool>
    {
        public BooleanExpression(string name) : base(name) { }
    }
}
