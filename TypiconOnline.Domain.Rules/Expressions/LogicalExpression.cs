using System.Collections.Generic;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Абстрактный класс для логических выражений
    /// </summary>
    public abstract class LogicalExpression : BooleanExpression
    {
        public LogicalExpression(string name) : base(name) { }

        public List<RuleExpression> ChildElements { get; set; } = new List<RuleExpression>();

        protected override void InnerInterpret(IRuleHandler handler)
        {
            RuleExpression exp1 = null;
            RuleExpression exp2 = null;

            bool? expValue = null;

            for (int i = 0; i < ChildElements.Count; i++)
            {
                exp2 = ChildElements[i];
                exp2.Interpret(handler);
                    
                if (exp1 != null)
                {
                    expValue = Operate(exp1, exp2, expValue);
                }

                exp1 = exp2;
            }

            ValueCalculated = (bool) expValue;
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
