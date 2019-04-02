using System;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class WeekNameQuery : IDataQuery<ItemText>
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
        public WeekNameQuery(DateTime date, bool isShortName)
        {
            Date = date;
            IsShortName = isShortName;
        }

        public DateTime Date { get; }
        public bool IsShortName { get; }
    }
}
