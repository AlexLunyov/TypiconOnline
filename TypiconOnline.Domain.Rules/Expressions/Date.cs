using System;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Простой класс. Внутри должна быть строка с датой без привязки к году --MM-dd
    /// Если пустое значение, то дата принимает текущее значение
    /// </summary>
    public class Date : DateExpression
    {        
        public Date(string name) : base(name) { }

        public ItemDate ValueExpression { get; set; } = new ItemDate();

        protected override void InnerInterpret(IRuleHandler handler)
        {
            //если значение выражения пустое, просто передаем текущую дату
            //из вводимой даты берем только год
            ValueCalculated = (ValueExpression.IsEmpty) 
                ? handler.Settings.Date 
                : new DateTime(handler.Settings.Date.Year, ValueExpression.Month, ValueExpression.Day);
        }

        protected override void Validate()
        {
            if (!ValueExpression.IsValid)
            {
                foreach (BusinessConstraint brokenRule in ValueExpression.GetBrokenConstraints())
                {
                    AddBrokenConstraint(brokenRule, ElementName);
                }
            }
        }
    }
}

