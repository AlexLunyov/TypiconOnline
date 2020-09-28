using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.AppServices.OutputFiltering;

namespace TypiconOnline.Web.Models.CustomSequenceModels
{
    public class CompleteSequenceViewModel : GetCustomSequenceViewModel
    {
        public CompleteSequenceViewModel() { }

        public CompleteSequenceViewModel(GetCustomSequenceViewModel model)
        {
            Id = model.Id;
            Language = model.Language;
            Date = model.Date;
            CustomSequence = model.CustomSequence;
        }

        public FilteredOutputDay Day { get; set; }
        public string StatusMessage { get; set; }
    }
}
