using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Output
{
    public class OutputDay : IHasId<int>
    {
        public int Id { get; set; }

        public int TypiconId { get; set; }
        /// <summary>
        /// Ссылка на Устав
        /// </summary>
        public virtual TypiconEntity Typicon { get; set; }

        /// <summary>
        /// Конкретная дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Наименование дня
        /// </summary>
        public virtual ItemText Name { get; set; }

        public int PredefinedSignId { get; set; }
        /// <summary>
        /// Ссылка на предустановленный Знак службы
        /// </summary>
        public virtual Sign PredefinedSign { get; set; }

        public int PrintDayTemplateId { get; set; }
        /// <summary>
        /// Номер предустановленного знака для отображения в расписании (может отличаться от указанного в <see cref="PredefinedSign"/>)
        /// </summary>
        public virtual PrintDayTemplate PrintDayTemplate { get; set; }

        //Добавить ссылки на тексты служб
        public virtual List<OutputDayWorship> OutputFormDayWorships { get; set; }

        /// <summary>
        /// Ссылки на Тексты служб, задействованных в формировании последовательности
        /// </summary>
        public IEnumerable<DayWorship> DayWorshipLinks
        {
            get
            {
                return (from drw in OutputFormDayWorships select drw.DayWorship).ToList();
            }
        }

        /// <summary>
        /// Службы с последовательностями богослужений, относящиеся к Дню
        /// </summary>
        public virtual List<OutputWorship> Worships { get; set; } = new List<OutputWorship>();
    }
}
