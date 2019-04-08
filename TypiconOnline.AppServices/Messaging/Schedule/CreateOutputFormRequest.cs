using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class CreateOutputFormRequest
    {
        public int TypiconId { get; set; }
        public int TypiconVersionId { get; set; }
        public DateTime Date { get; set; }
        public HandlingMode HandlingMode { get; set; }
    }
}
