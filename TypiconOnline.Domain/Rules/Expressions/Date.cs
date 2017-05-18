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
    /// </summary>
    public class Date : DateExpression
    {
        public Date(XmlNode node) : base(node)
        {
            //формат вводимой даты должен быть такой --MM-dd
            //--04-07
            if (node != null)
            {
                _valueExpression = new ItemDate(node.InnerText);
            }

            //Regex rgx = new Regex(@"--\d\d-\d\d");//(@"-(-\d{ 2}){ 2}");

            //if (!rgx.IsMatch(node.InnerText))
            //{
            //    AddBrokenConstraint(DateBusinessRule.DateTypeMismatch, node.Name);
            //    //throw new DefinitionsParsingException("Ошибка: неверно введен формат даты. Правильный формат: --MM-dd");
            //}

            //if (!DateTime.TryParseExact(node.InnerText, "--MM-dd", new CultureInfo("ru-RU"), DateTimeStyles.None, out _outputDate))
            //{
            //    AddBrokenConstraint(DateBusinessRule.DateTypeMismatch, node.Name);
            //    //throw new DefinitionsParsingException("Ошибка: неверно введена дата в элементе " + node.Name);
            //}
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
                //из вводимой даты берем только год
                _valueCalculated = new DateTime(date.Year, ((ItemDate)_valueExpression).Month, ((ItemDate)_valueExpression).Day);
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

