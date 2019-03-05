using System;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Typicon
{
    public class MenologyRule : DayRule
    {
        public MenologyRule()
        {
            //Date = new ItemDate();
            //DateB = new ItemDate();
        }

        public virtual ItemDate Date { get; set; }

        public virtual ItemDate DateB { get; set; }

        protected override void Validate()
        {
            base.Validate();

            if (!Date.IsValid)
            {
                AppendAllBrokenConstraints(Date, "Date");
            }

            if (!DateB.IsValid)
            {
                AppendAllBrokenConstraints(Date, "DateB");
            }
        }

        /// <summary>
        /// Возвращает конкретную дату в году, когда совершается данная служба
        /// </summary>
        /// <param name="year">Конкретный год</param>
        /// <returns>Если поля Date или DateB пустые, вовращает пустое (минимальное) значение</returns>
        public DateTime GetCurrentDate(int year)
        {
            if (IsValid) //&& /*!Date.IsEmpty && */!DateB.IsEmpty)
            {
                if (DateTime.IsLeapYear(year))
                {
                    return (!DateB.IsEmpty) ? new DateTime(year, DateB.Month, DateB.Day) : DateTime.MinValue;
                }
                else
                {
                    return (!Date.IsEmpty) ? new DateTime(year, Date.Month, Date.Day) : DateTime.MinValue;
                }
            }

            return DateTime.MinValue;
        }
    }
}
