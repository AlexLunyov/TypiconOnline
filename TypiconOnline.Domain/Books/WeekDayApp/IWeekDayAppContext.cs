namespace TypiconOnline.Domain.Books.WeekDayApp
{
    public interface IWeekDayAppContext
    {
        GetWeekDayResponse Get(GetWeekDayRequest request);
    }
}
