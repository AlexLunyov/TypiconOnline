using System;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public abstract class BooleanExpression : RuleExpression
    {
        public BooleanExpression(string name) : base(name) { }

        //protected int _outputValue;

        public override Type ExpressionType
        {
            get
            {
                return typeof(bool);
            }
        }

        //public override int OutputValue
        //{
        //    get { return _outputValue; }
        //}

    }
}
