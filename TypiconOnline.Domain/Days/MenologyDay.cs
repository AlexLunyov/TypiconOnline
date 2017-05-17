using System;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Days
{
    public class MenologyDay : Day
    {
        public virtual ItemDate Date
        {
            get;
            set;
        }

        public virtual ItemDate DateB
        {
            get;
            set;
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
                    return new DateTime(year, DateB.Month, DateB.Day);
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
            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenConstraint(MenologyDayBusinessConstraint.MenologyDayNameRequired);
            }

            if (!Date.IsValid)
            {
                foreach (BusinessConstraint principle in Date.GetBrokenConstraints())
                {
                    AddBrokenConstraint(principle, "Date");
                }
            }

            if (!DateB.IsValid)
            {
                foreach (BusinessConstraint principle in DateB.GetBrokenConstraints())
                {
                    AddBrokenConstraint(principle, "DateB");
                }
            }
        }

    }
}

