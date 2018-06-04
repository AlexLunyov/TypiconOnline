using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class WeekNameQuery : IQuery<ItemTextUnit>
    {
        /// <summary>
        /// Возвращает наименование седмицы (вставляется в шапку шаблона седмицы)
        /// Примеры: Седмица 33-ая по Пятидесятнице
        ///          Седмица 6-ая по Пасхе
        ///          Седмица 3-ая Великого поста
        /// </summary>
        /// <param name="date">Дата для проверки</param>
        /// <param name="isShortName">Если true, возвращает краткое название - для файлов.</param
        /// <returns></returns>
        public WeekNameQuery(DateTime date, string language, bool isShortName)
        {
            Date = date;
            Language = language;
            IsShortName = isShortName;
        }

        public DateTime Date { get; }
        public string Language { get; }
        public bool IsShortName { get; }
    }
}
