using System;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Oktoikh
{
    public interface IOktoikhContext
    {
        OktoikhDay Get(DateTime date);
        int CalculateIhosNumber(DateTime date);
        ItemTextUnit GetSundayName(DateTime date, string language, string stringToPaste = null);
        ItemTextUnit GetWeekName(DateTime date, string language, bool isShortName);
    }
}
