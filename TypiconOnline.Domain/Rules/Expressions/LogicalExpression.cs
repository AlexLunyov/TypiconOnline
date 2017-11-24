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
        public LogicalExpression(string name) : base(name) { }
        public LogicalExpression(XmlNode node) : base(node)
        {
            if (node.HasChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    ChildElements.Add(RuleFactory.CreateExpression(childNode));
                }
            }
        }

        public List<RuleExpression> ChildElements { get; set; } = new List<RuleExpression>();

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            RuleExpression exp1 = null;
            RuleExpression exp2 = null;

            bool? expValue = null;

            for (int i = 0; i < ChildElements.Count; i++)
            {
                exp2 = ChildElements[i];
                exp2.Interpret(date, handler);
                    
                if (exp1 != null)
                {
                    expValue = Operate(exp1, exp2, expValue);
                }

                exp1 = exp2;
            }

            ValueCalculated = expValue;
        }

        /// <summary>
        /// Метод должен быть реализован во всех наследниках. Используется в методе InnerInterpret
        /// </summary>
        /// <param name="previousValue">предыдущее значение в цепочке</param>
        /// <returns></returns>
        protected abstract bool Operate(RuleExpression exp1, RuleExpression exp2, bool? previousValue);

        protected override void Validate()
        {
            if (ChildElements.Count == 0)
            {
                AddBrokenConstraint(LogicalExpressionBusinessConstraint.ChildrenRequired, ElementName);
            }
            if (ChildElements.Count < 2)
            {
                AddBrokenConstraint(LogicalExpressionBusinessConstraint.NotEnoughChildren, ElementName);
            }

            RuleExpression exp1 = null;
            RuleExpression exp2 = null;

            for (int i = 0; i < ChildElements.Count; i++)
            {
                exp2 = ChildElements[i];
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
