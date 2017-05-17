using TypiconOnline.AppServices.Messaging.Schedule;

namespace TypiconOnline.AppServices.Services
{
    public interface IScheduleService
    {
        GetScheduleDayResponse GetScheduleDay(GetScheduleDayRequest request);
        GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request);
    }
}
