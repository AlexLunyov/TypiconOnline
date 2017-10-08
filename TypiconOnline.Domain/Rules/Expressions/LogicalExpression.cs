using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Абстрактный класс для логических выражений
    /// </summary>
    public abstract class LogicalExpression : BooleanExpression
    {
        protected List<RuleExpression> _childElements = new List<RuleExpression>();

        public LogicalExpression(XmlNode node) : base(node)
        {
            if (node.HasChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    _childElements.Add(RuleFactory.CreateExpression(childNode));
                }
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            RuleExpression exp1 = null;
            RuleExpression exp2 = null;

            bool? expValue = null;

            for (int i = 0; i < _childElements.Count; i++)
            {
                exp2 = _childElements[i];
                exp2.Interpret(date, handler);
                    
                if (exp1 != null)
                {
                    expValue = Operate(exp1, exp2, expValue);
                }

                exp1 = exp2;
            }

            _valueCalculated = expValue;
        }

        /// <summary>
        /// Метод должен быть реализован во всех наследниках. Используется в методе InnerInterpret
        /// </summary>
        /// <param name="previousValue">предыдущее значение в цепочке</param>
        /// <returns></returns>
        protected abstract bool Operate(RuleExpression exp1, RuleExpression exp2, bool? previousValue);

        protected override void Validate()
        {
            if (_childElements.Count == 0)
            {
                AddBrokenConstraint(LogicalExpressionBusinessConstraint.ChildrenRequired, ElementName);
            }
            if (_childElements.Count < 2)
            {
                AddBrokenConstraint(LogicalExpressionBusinessConstraint.NotEnoughChildren, ElementName);
            }

            RuleExpression exp1 = null;
            RuleExpression exp2 = null;

            for (int i = 0; i < _childElements.Count; i++)
            {
                exp2 = _childElements[i];
                if (exp2 == null)
                {
                    AddBrokenConstraint(LogicalExpressionBusinessConstraint.InvalidChild, ElementName);
                }
                else
                {
                    //Проверяем, элемент с каким выходным типом значения
                    //сравниваем ExpressionType элементов
                    if (exp1?.ExpressionTypeEquals(exp2) == false)
                    {
                        AddBrokenConstraint(LogicalExpressionBusinessConstraint.TypeMismatch, ElementName);
                    }
                }
                exp1 = exp2;
            }
        }
    }
}
