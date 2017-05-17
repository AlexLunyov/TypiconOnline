using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Easter
{
    /// <summary>
    /// Хранилище дней Пасхи
    /// </summary>
    public class EasterStorage 
    {
        private EasterStorage()
        {
        }

        //private List<EasterItem> _easterDays;

        public List<EasterItem> EasterDays { get; set;
            //get
            //{
            //    return _easterDays;
            //} 
        }

        public DateTime GetCurrentEaster(int year)
        {
            EasterItem easter = EasterDays.Find(c => c.Date.Year == year);
            if (easter == null)
                throw new NullReferenceException("День празднования Пасхи не определен для года " + year);

            return easter.Date;
        }

        /// <summary>
        /// Метод сделан только для тестирования
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Возвращает ту же дату</returns>
        public DateTime GetCurrentEaster(DateTime date)
        {
            return date;
        }

        public static EasterStorage Instance
        {
            get { return Nested.instance; }
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly EasterStorage instance = new EasterStorage();
        }
    }
}
