namespace TypiconOnline.Domain.Rules.Expressions
{
    /* EXAMPLE
     * <and>
		    <equals><int>1</int><daysfromeaster><date>--04-04</date></daysfromeaster></equals>
		    <more><int>2</int><int>1</int></more>
	    </and>
     * 
     */

    /// <summary>
    /// Вычисляет логическую операцию AND
    /// </summary>
    public class And : LogicalOperation
    {
        public And(string name) : base(name) { }

        protected override bool Operate(RuleExpression exp1, RuleExpression exp2, bool? previousValue)
        {
            return (previousValue != null) ? ((bool)exp1.ValueCalculated && (bool)exp2.ValueCalculated && (bool)previousValue)
                : ((bool)exp1.ValueCalculated && (bool)exp2.ValueCalculated);
        }
    }
}
