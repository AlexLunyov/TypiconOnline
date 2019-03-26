using System;

namespace TypiconOnline.Domain.Rules.Expressions
{
    
    public abstract class DateExpression : RuleExpression<DateTime>
    {
        public DateExpression(string name) : base(name) { }
    }
}
