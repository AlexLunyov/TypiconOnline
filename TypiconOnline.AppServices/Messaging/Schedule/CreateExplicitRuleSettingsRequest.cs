using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class CreateExplicitRuleSettingsRequest : ScheduleDataCalculatorRequest
    {
        public CreateExplicitRuleSettingsRequest() { }

        public CreateExplicitRuleSettingsRequest(ScheduleDataCalculatorRequest request)
        {

            TypiconVersionId = request.TypiconVersionId;
            Date = request.Date;
            ApplyParameters = request.ApplyParameters;
            CheckParameters = request.CheckParameters;
        }

        public ExplicitAddRule Rule { get; set; }
    }
}
