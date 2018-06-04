using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Запрос по имени Катавасии возвращает текст Канона, где определены только катавасии
    /// </summary>
    public class KatavasiaQuery : IDataQuery<Kanonas>
    {
        public KatavasiaQuery(string name)
        {
            Name = name;
        }
        public string Name { get;}
    }
}
