using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Typicon
{
    public class MenologyRule : TypiconRule
    {
        private MenologyDay _day;
        public virtual MenologyDay Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value;

                //if (value != null)
                //{
                //    Date = value.Date;
                //    DateB = value.DateB;
                //}
            }
        }

        /// <summary>
        /// Назначенный знак для этого описания
        /// </summary>
        //public virtual Sign Sign { get; set; }

        //public new FolderEntity Folder { get; set; }

        //отсутвует хранение xml-формы правила

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        //internal ItemDate Date;

        //internal ItemDate DateB;

        /// <summary>
        /// Возвращает конкретную дату в году, когда совершается данная служба
        /// </summary>
        /// <param name="year">Конкретный год</param>
        /// <returns>Если поля Date или DateB пустые, вовращает пустое (минимальное) значение</returns>
        //public DateTime GetCurrentDate(int year)
        //{
        //    if (!Date.IsEmpty && !DateB.IsEmpty)
        //    {
        //        return (DateTime.IsLeapYear(year)) ? new DateTime(year, DateB.Month, DateB.Day) : new DateTime(year, Date.Month, Date.Day);
        //    }

        //    return DateTime.MinValue;
        //}
    }
}
