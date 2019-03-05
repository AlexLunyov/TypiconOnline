using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    public class CreateModifiedYearJob : JobBase
    {
        public int TypiconId { get; set; }
        public int Year { get; set; }
    }
}
