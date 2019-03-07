using System;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Days
{
    public class MenologyDay : Day
    {
        /// <summary>
        /// Дата
        /// </summary>
        public virtual ItemDate Date { get; set; } = new ItemDate();
        /// <summary>
        /// Дата для високосного года
        /// </summary>
        public virtual ItemDate LeapDate { get; set; } = new ItemDate();

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
                    return (!LeapDate.IsEmpty) ? new DateTime(year, LeapDate.Month, LeapDate.Day) : DateTime.MinValue;
                }
                else
                {
                    return (!Date.IsEmpty) ? new DateTime(year, Date.Month, Date.Day) : DateTime.MinValue;
                }
            }
            
            return DateTime.MinValue;
        }

        protected override void Validate()
        {
            base.Validate();

            if (!Date.IsValid)
            {
                AppendAllBrokenConstraints(Date, "Date");
            }

            if (!LeapDate.IsValid)
            {
                AppendAllBrokenConstraints(Date, "DateB");
            }
        }

    }
}

