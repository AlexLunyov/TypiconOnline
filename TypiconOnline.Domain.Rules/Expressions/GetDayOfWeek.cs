using System;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Expressions
{
    /// <summary>
    /// Элемент содержит описание дня недели
    /// Возвращает день недели у абстрактной даты
    /// </summary>
    /// <example>
    ///     <dayofweek>воскресенье</dayofweek>
    /// 
    ///     <getdayofweek><date>--01-04</date></getdayofweek>
    ///     
    ///     <getdayofweek><getclosestday dayofweek="saturday" weekcount="-2"><date>--11-08</date></getclosestday></getdayofweek>
    /// 
    ///     <getdayofweek name="суббота"/>
    /// </example>
    public class GetDayOfWeek : RuleExpression<DayOfWeek>
    {
        public GetDayOfWeek(string name) : base(name) { }

        public DayOfWeek? DayOfWeek { get; set; }

        public DateExpression ChildDateExp { get; set; }
        
        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (ChildDateExp != null)
            {
                ChildDateExp.Interpret(handler);

                ValueCalculated = ChildDateExp.ValueCalculated.DayOfWeek;
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

            //if ((DayOfWeek != null) && (!DayOfWeek.IsValid))
            //{
            //    AddBrokenConstraint(GetDayOfWeekBusinessConstraint.DayOfWeekWrongDefinition, ElementName);
            //}

            //добавляем ломаные правила к родителю
            if (ChildDateExp?.IsValid == false)
            {
                AppendAllBrokenConstraints(ChildDateExp, ElementName);
            }
        }
    }
}
