using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class ScheduleWeekSettingsQuery : IQuery<Result<ScheduleSettingsWeekDaysModel>>, IHasAuthorizedAccess
    {
        public ScheduleWeekSettingsQuery(int typiconId)
        {
            TypiconId = typiconId;

            Key = new TypiconEntityCanEditKey(TypiconId);
        }

        public int TypiconId { get; set; }

        public IAuthorizeKey Key { get; }
    }
}
