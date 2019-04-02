using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOutputForms
    {
        //Task<Result<ScheduleDay>> GetVersion(int typiconVersionId, DateTime date, UserInfo userInfo);
        Result<LocalizedOutputDay> Get(int typiconId, DateTime date, string language, HandlingMode handlingMode = HandlingMode.AstronomicDay);
        Result<LocalizedOutputWeek> GetWeek(int typiconId, DateTime date, string language);
    }
}
