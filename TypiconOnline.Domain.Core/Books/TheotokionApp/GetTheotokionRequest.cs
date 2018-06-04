using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
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
    }
}
