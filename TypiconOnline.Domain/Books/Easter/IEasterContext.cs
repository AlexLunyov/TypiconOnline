using System;
using System.Collections.Generic;

namespace TypiconOnline.Domain.Books.Easter
{
    public interface IEasterContext
    {
        DateTime GetCurrentEaster(int year);

        IEnumerable<EasterItem> GetAll();
        /// <summary>
        /// Возвращает количество дней относительно Пасхи по году, соответствующему введенной дате
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        int GetDaysFromCurrentEaster(DateTime date);
    }
}
