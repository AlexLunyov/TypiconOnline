using System;
using System.Collections.Generic;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public abstract class RuleExpression : RuleElementBase
    {
        public RuleExpression(string name) : base(name) { }

        public abstract bool ExpressionTypeEquals(RuleExpression entity);

        public abstract bool ValueCalculatedEquals(RuleExpression entity);
    }

    public abstract class RuleComparableExpression : RuleExpression, IComparable<RuleComparableExpression>
    {
        public RuleComparableExpression(string name) : base(name) { }

        public abstract int CompareTo(RuleComparableExpression other);
    }

    public abstract class RuleExpression<T> : RuleComparableExpression, IEquatable<RuleExpression<T>> where T: IComparable
    {
        public RuleExpression(string name) : base(name) { }

        /// <summary>
        /// Вычисленное значение
        /// </summary>
        public virtual T ValueCalculated { get; protected set; }

        public override int CompareTo(RuleComparableExpression other)
        {
            if (other is RuleExpression<T> exp)
            {
                return exp.ValueCalculated.CompareTo(ValueCalculated);
            }
            else
            {
                //проверить - вроде как не правильно это
                //выводить 0, если типы даже не сравниваются...
                throw new InvalidCastException("Неверное сравнение типов вычисляемых значений");
            }
        }

        public override bool ExpressionTypeEquals(RuleExpression entity)
        {
            return (entity != null) && (entity is RuleExpression<T>);
        }

        public override bool ValueCalculatedEquals(RuleExpression entity)
        {
            return Equals(entity);
        }

        //public virtual TExpr ExpressionType { get; }

        #region Equals

        public override bool Equals(object entity)
        {
            return entity != null
               && entity is RuleExpression<T> exp
               && Equals(exp);
        }

        public bool Equals(RuleExpression<T> other)
        {
            return other != null && EqualityComparer<T>.Default.Equals(ValueCalculated, other.ValueCalculated);
        }

        public override int GetHashCode()
        {
            return -1617952366 + EqualityComparer<T>.Default.GetHashCode(ValueCalculated);
        }

        public static bool operator ==(RuleExpression<T> expression1, RuleExpression<T> expression2)
        {
            return EqualityComparer<RuleExpression<T>>.Default.Equals(expression1, expression2);
        }

        public static bool operator !=(RuleExpression<T> expression1, RuleExpression<T> expression2)
        {
            return !(expression1 == expression2);
        }

        #endregion
    }
    
}

