using TypiconOnline.AppServices.Messaging.Schedule;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleService
    {
        GetScheduleDayResponse GetScheduleDay(GetScheduleDayRequest request);
        GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request);
    }
}
