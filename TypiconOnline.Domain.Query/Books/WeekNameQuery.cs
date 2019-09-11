using System;
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
        /// <param name="typiconId">Id Устава</param>
        /// <param name="date">Дата для проверки</param>
        /// <param name="isShortName">Если true, возвращает краткое название - для файлов.</param
        /// <returns></returns>
        public WeekNameQuery(int typiconId, DateTime date, bool isShortName, string language = "cs-ru")
        {
            TypiconId = typiconId;
            Date = date;
            IsShortName = isShortName;
            Language = language;
        }
        public int TypiconId { get; }
        public DateTime Date { get; }
        public bool IsShortName { get; }
        public string Language { get; }
    }
}
