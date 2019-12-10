using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class MenologyRule : DayRule
    {
        private ItemDate _date;
        private ItemDate _leapDate; 

        public MenologyRule()
        {
            Date = new ItemDate();
            LeapDate = new ItemDate();
        }

        public virtual ItemDate Date //{ get; set; }
        {
            get
            {
                if (_date == null)
                {
                    _date = new ItemDate();
                }
                return _date;
            }
            set => _date = value;
        }

        public virtual ItemDate LeapDate //{ get; set; }
        {
            get
            {
                if (_leapDate == null)
                {
                    _leapDate = new ItemDate();
                }
                return _leapDate;
            }
            set => _leapDate = value;
        }

        /// <summary>
        /// Список на используемые в данном Правиле Переменные Устава
        /// </summary>
        public virtual List<VariableModRuleLink<MenologyRule>> VariableLinks { get; set; }

        /// <summary>
        /// Список на используемые в определении данного Правила Печатные шаблоны
        /// </summary>
        public virtual List<PrintTemplateModRuleLink<MenologyRule>> PrintTemplateLinks { get; set; }

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            base.Validate(serializerRoot);

            if (!Date.IsValid)
            {
                AppendAllBrokenConstraints(Date, "Date");
            }

            if (!LeapDate.IsValid)
            {
                AppendAllBrokenConstraints(Date, "LeapDate");
            }

            if (DayRuleWorships.Count > 3)
            {
                AddBrokenConstraint(new BusinessConstraint("В коллекции Текстов служб не должно быть более трех Текстов служб", "MenologyRule"));
            }
        }

        /// <summary>
        /// Возвращает конкретную дату в году, когда совершается данная служба
        /// </summary>
        /// <param name="year">Конкретный год</param>
        /// <returns>Если поля Date или DateB пустые, вовращает пустое (минимальное) значение</returns>
        public DateTime GetCurrentDate(int year)
        {
            if (DateTime.IsLeapYear(year))
            {
                return (!LeapDate.IsEmpty) ? new DateTime(year, LeapDate.Month, LeapDate.Day) : DateTime.MinValue;
            }
            else
            {
                return (!Date.IsEmpty) ? new DateTime(year, Date.Month, Date.Day) : DateTime.MinValue;
            }
        }
    }
}
