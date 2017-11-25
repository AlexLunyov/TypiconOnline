using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Элемент содержит описание дня недели
    /// Возвращает день недели у абстрактной даты
    /// 
    /// Примеры
    /// <dayofweek>воскресенье</dayofweek>
    /// 
    /// <getdayofweek><date>--01-04</date></getdayofweek>
    /// <getdayofweek><getclosestday dayofweek="saturday" weekcount="-2"><date>--11-08</date></getclosestday></getdayofweek>
    /// <getdayofweek name="суббота"/>
    /// </summary>
    public class GetDayOfWeek : RuleExpression
    {
        public GetDayOfWeek(string name) : base(name) { }

        public ItemDayOfWeek DayOfWeek { get; set; }

        public DateExpression ChildDateExp { get; set; }

        public override Type ExpressionType
        {
            get
            {
                return typeof(DayOfWeek);
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (ChildDateExp != null)
            {
                ChildDateExp.Interpret(date, handler);

                ValueCalculated = ((DateTime)ChildDateExp.ValueCalculated).DayOfWeek;
            }
            else
            {
                ValueCalculated = DayOfWeek.Value;
            }
        }

        protected override void Validate()
        {
            if ((DayOfWeek == null) && (ChildDateExp == null))
            {
                AddBrokenConstraint(GetDayOfWeekBusinessConstraint.TermsRequired, ElementName);
            }

            if ((DayOfWeek != null) && (ChildDateExp != null))
            {
                AddBrokenConstraint(GetDayOfWeekBusinessConstraint.TermsTooMuch, ElementName);
            }

            if ((DayOfWeek != null) && (!DayOfWeek.IsValid))
            {
                AddBrokenConstraint(GetDayOfWeekBusinessConstraint.DayOfWeekWrongDefinition, ElementName);
            }

            //добавляем ломаные правила к родителю
            if (ChildDateExp?.IsValid == false)
            {
                AppendAllBrokenConstraints(ChildDateExp, ElementName);
            }
        }
    }
}
