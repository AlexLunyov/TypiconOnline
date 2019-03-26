namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Вычисляет логическую операцию OR
    /// </summary>
    public class Or : LogicalOperation
    {
        public Or(string name) : base(name) { }

        protected override bool Operate(RuleExpression exp1, RuleExpression exp2, bool? previousValue)
        {
            bool exp = ((BooleanExpression)exp1).ValueCalculated || ((BooleanExpression)exp2).ValueCalculated;

            return (previousValue != null) ? (exp || (bool)previousValue) : exp;
        }
    }
}
