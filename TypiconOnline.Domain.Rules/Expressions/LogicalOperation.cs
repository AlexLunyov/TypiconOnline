﻿namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Базовый класс для логических операций: И ИЛИ
    /// </summary>
    public abstract class LogicalOperation : LogicalExpression
    {
        public LogicalOperation(string name) : base(name) { }

        protected override void Validate()
        {
            base.Validate();

            foreach (RuleExpression exp in ChildElements)
            {
                //Проверяем, элемент с каким выходным типом значения
                //Должен быть только булевским
                if ((exp != null) && !(exp is BooleanExpression))
                {
                    AddBrokenConstraint(LogicalExpressionBusinessConstraint.TypeBooleanMismatch, ElementName);
                }
            }
        }
    }
}
