using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.WeekDayApp
{
    public class GetWeekDayResponse : ServiceResponseBase
    {
        public WeekDayApp WeekDayApp { get; set; }
    }
}