using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class SundayNameQuery : IDataQuery<ItemTextUnit>
    {
        /// <summary>
        /// Возвращает строку с наименованием воскресного дня. 
        /// Используется для вставки в ряд воскресного дня в расписании на неделю
        /// </summary>
        /// <param name="date">Вводимая дата</param>
        /// <param name="language">Язык локализации</param>
        /// <param name="stringToPaste">Строка, которая будет вставлена после названия Недели, перед гласом</param>
        /// <returns>Возвращает строку с наименованием воскресного дня. </returns>
        public SundayNameQuery(DateTime date, string language, string stringToPaste = null)
        {
            Date = date;
            Language = language;
            StringToPaste = stringToPaste;
        }
        public DateTime Date { get; }
        public string Language { get; }
        public string StringToPaste { get; }
    }
}
