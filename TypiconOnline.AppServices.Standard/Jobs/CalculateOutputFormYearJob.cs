using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    public class CalculateOutputFormYearJob : JobBase
    {
        public CalculateOutputFormYearJob(int typiconId, int typiconVersionId, int year) : base()
        {
            TypiconId = typiconId;
            TypiconVersionId = typiconVersionId;
            Year = year;
        }

        /// <summary>
        /// Id Устава
        /// </summary>
        public int TypiconId { get; }
        /// <summary>
        /// Версия Устава
        /// </summary>
        public int TypiconVersionId { get; }
        public int Year { get; }
    }
}
