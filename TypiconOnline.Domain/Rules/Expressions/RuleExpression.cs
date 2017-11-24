using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Expressions
{ 
    public abstract class RuleExpression : RuleElement
    {
        public RuleExpression(string name) : base(name) { }
        public RuleExpression(XmlNode node) : base(node) { }

        public virtual object ValueExpression { get; set; }

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

