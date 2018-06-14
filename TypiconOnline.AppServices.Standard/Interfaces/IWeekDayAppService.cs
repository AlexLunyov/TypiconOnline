using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.WeekDayApp;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IWeekDayAppService : IWeekDayAppContext
    {
        AddWeekDayAppResponse Insert(AddWeekDayAppRequest request);
        UpdateWeekDayAppResponse Update(UpdateWeekDayAppRequest request);
        RemoveWeekDayAppResponse Delete(RemoveWeekDayAppRequest request);
    }
}
