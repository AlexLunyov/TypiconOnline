using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOutputForms
    {
        //Task<Result<ScheduleDay>> GetVersion(int typiconVersionId, DateTime date, UserInfo userInfo);
        Result<ScheduleDay> Get(int typiconId, DateTime date, HandlingMode handlingMode = HandlingMode.AstronomicDay);
        Result<ScheduleWeek> GetWeek(int typiconId, DateTime date);
    }
}
