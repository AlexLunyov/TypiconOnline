using System;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public abstract class IntExpression : RuleExpression
    {
        public IntExpression(string name) : base(name) { }

        public override Type ExpressionType => typeof(int);
    }
}
