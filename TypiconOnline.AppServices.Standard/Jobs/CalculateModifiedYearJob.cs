using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    public class CalculateModifiedYearJob : JobBase
    {
        public CalculateModifiedYearJob(int typiconVersionId, int year) : base()
        {
            TypiconVersionId = typiconVersionId;
            Year = year;
        }

        public int TypiconVersionId { get; }
        public int Year { get; }
    }
}
