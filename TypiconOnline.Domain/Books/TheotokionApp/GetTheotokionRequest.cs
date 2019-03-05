using System;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.TheotokionApp
{
    /// <summary>
    /// Запрос для получения Богородична из приложений книги Иромологий
    /// </summary>
    public class GetTheotokionRequest: ServiceRequestBase
    {
        public TheotokionAppPlace Place { get; set; }
        public int Ihos { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public ITypiconSerializer Serializer { get; set; }
    }
}
