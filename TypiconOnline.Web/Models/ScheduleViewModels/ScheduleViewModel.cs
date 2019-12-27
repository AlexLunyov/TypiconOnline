using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.Web.Models.ScheduleViewModels
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public DateTime Date { get; set; }
        public Result<FilteredOutputWeek> Week { get; set; }
    }
}
