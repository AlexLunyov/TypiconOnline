using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Базовый класс для логинческих опреаций: И ИЛИ
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
