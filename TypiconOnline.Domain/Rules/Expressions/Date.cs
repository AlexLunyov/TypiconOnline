using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Простой класс. Внутри должна быть строка с датой без привязки к году --MM-dd
    /// Если пустое значение, то дата принимает текущее значение
    /// </summary>
    public class Date : DateExpression
    {
        public Date(XmlNode node) : base(node)
        {
            _valueExpression = new ItemDate(node.InnerText);
        }

        public override object ValueCalculated
        {
            get
            {
                return (IsValid) ? base.ValueCalculated : DateTime.MinValue;
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler settings)
        {
            if (IsValid)
            {
                //если значение выражения пустое, просто передаем текущую дату
                //из вводимой даты берем только год
                _valueCalculated = (((ItemDate)_valueExpression).IsEmpty) ? date : new DateTime(date.Year, ((ItemDate)_valueExpression).Month, ((ItemDate)_valueExpression).Day);
            }
        }

        protected override void Validate()
        {
            if (!((ItemDate)_valueExpression).IsValid)
            {
                foreach (BusinessConstraint brokenRule in ((ItemDate)_valueExpression).GetBrokenConstraints())
                {
                    AddBrokenConstraint(brokenRule, ElementName);
                }
            }
        }
    }
}

