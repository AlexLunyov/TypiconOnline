using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    /// <summary>
    /// Запрос на формирование выходных форм для указанного Устава и на указанный год
    /// </summary>
    public class OutputFormYearCalculatorRequest
    {
        public OutputFormYearCalculatorRequest(int typiconId, int year)
        {
            TypiconId = typiconId;
            Year = year;
        }

        public int TypiconId { get; }
        public int Year { get; }
    }
}
