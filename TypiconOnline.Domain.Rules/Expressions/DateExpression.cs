using System;

namespace TypiconOnline.Domain.Rules.Expressions
{
    
    public abstract class DateExpression : RuleExpression
    {
        public DateExpression(string name) : base(name) { }

        //protected DateTime _outputDate = DateTime.MinValue;

        #region Properties

        public override Type ExpressionType
        {
            get
            {
                return typeof(DateTime);
            }
        }
        

        //public override DateTime ValueCalculated
        //{
        //    get
        //    {
        //        return _outputDate;
        //    }
        //}

        #endregion
    }
}
