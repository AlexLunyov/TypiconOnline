using System;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class SundayNameQuery : IDataQuery<ItemText>
    {
        /// <summary>
        /// Возвращает строку с наименованием воскресного дня. 
        /// Используется для вставки в ряд воскресного дня в расписании на неделю
        /// </summary>
        /// <param name="date">Вводимая дата</param>
        /// <param name="language">Язык локализации</param>
        /// <param name="stringToPaste">Строка, которая будет вставлена после названия Недели, перед гласом</param>
        /// <returns>Возвращает строку с наименованием воскресного дня. </returns>
        public SundayNameQuery(DateTime date, ItemText textToPaste = null)
        {
            Date = date;
            TextToPaste = textToPaste;
        }
        public DateTime Date { get; }
        public ItemText TextToPaste { get; }
    }
}
