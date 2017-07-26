using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Typicon
{
    public class MenologyRule : DayRule
    {
        //public virtual MenologyDay Day { get; set; }

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
        /// Строка сожержит номера имен Дня, использующихся в Правиле, разделенных запятою
        /// Пример: 
        /// 1,3
        /// 1,2,3
        /// </summary>
        //public virtual string SelectedNames { get; set; }

        //public override string Name
        //{
        //    get
        //    {
        //        return GetName(Day, SelectedNames);
        //    }
        //}

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
