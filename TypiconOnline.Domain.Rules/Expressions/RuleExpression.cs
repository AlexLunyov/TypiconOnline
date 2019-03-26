using System;

namespace TypiconOnline.Domain.Rules.Expressions
{ 
    public abstract class RuleExpression : RuleElementBase
    {
        public RuleExpression(string name) : base(name) { }

        /// <summary>
        /// Значение, введенное в определении Правила
        /// </summary>
        public virtual object ValueExpression { get; set; }
        /// <summary>
        /// Значение, вычисленное исходя из дочерних элементов или <see cref="ValueExpression"/>
        /// </summary>
        public virtual object ValueCalculated { get; protected set; }
        public virtual Type ExpressionType { get; }

        public virtual bool ValueExpressionEquals(RuleExpression entity)
        {
            return (entity != null)
               //&& entity is RuleExpression
               && ValueExpression.Equals(entity.ValueExpression);
               //&& this == (RuleExpression<ExpType>)entity;
        }

        //public virtual bool ValueCalculatedEquals(RuleExpression exp)
        //{
        //    return (exp?.ValueCalculatedEquals(this) == true);
        //}

        /// <summary>
        /// Метод сравнивает типы выражений объектов
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ExpressionTypeEquals(RuleExpression obj)
        {
            return (obj != null)
                && ExpressionType.Equals(obj.ExpressionType);
        }

        //public bool OutputValueEquals(RuleExpression obj)
        //{
        //    if (obj == null)
        //        return false;

        //    if (OutputType.Equals(obj.OutputType))
        //    {
        //        return OutputValue.Equals(obj.OutputValue);
        //    }

        //    return false;
        //}
    }
}

