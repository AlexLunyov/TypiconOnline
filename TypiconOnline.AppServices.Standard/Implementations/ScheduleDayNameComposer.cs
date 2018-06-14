using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Implementations
{
    public class ScheduleDayNameComposer : IScheduleDayNameComposer
    {
        IDataQueryProcessor queryProcessor;

        public ScheduleDayNameComposer([NotNull] IDataQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        public ItemTextUnit Compose(DateTime date, int seniorRulePriority, ICollection<DayWorship> dayWorships, LanguageSettings language)
        {
            var result = new ItemTextUnit() { Language = language.Name };

            if (dayWorships == null || dayWorships.Count == 0)
            {
                return result;
            }

            string resultString = "";

            DayWorship seniorService = dayWorships.First();

            //собираем все имена текстов, кроме главного
            if (dayWorships.Count > 1)
            {
                for (int i = 1; i < dayWorships.Count; i++)
                {
                    resultString += dayWorships.ElementAt(i).WorshipName.FirstOrDefault(language.Name).Text + " ";
                }
            }

            //а теперь разбираемся с главным

            string s = seniorService.WorshipName.FirstOrDefault(language.Name).Text;

            if (date.DayOfWeek != DayOfWeek.Sunday
                || (date.DayOfWeek == DayOfWeek.Sunday
                    && (seniorService.UseFullName || seniorService.WorshipShortName.IsEmpty)))
            {
                resultString = $"{s} {resultString}";
            }

            //Воскресный день

            if (/*(settings.Rule is MenologyRule)
                && */(date.DayOfWeek == DayOfWeek.Sunday)
                && (seniorRulePriority > 1))
            {
                //Если Триоди нет и воскресенье, находим название Недели из Октоиха
                //и добавляем название Недели в начало Name

                //Если имеется короткое название, то добавляем только его
                var shortName = GetShortName(dayWorships, language.Name);

                var sundayName = queryProcessor.Process(new SundayNameQuery(date, language, shortName));

                resultString = sundayName.Text + " " + resultString;
            }

            result.Text = resultString;

            return result;
        }

        public ItemTextUnit GetWeekName(DateTime date, string language) => queryProcessor.Process(new WeekNameQuery(date, language, false));

        private string GetShortName(ICollection<DayWorship> dayServices, string language)
        {
            string result = "";

            for (int i = 0; i < dayServices.Count; i++)
            {
                string s = dayServices.ElementAt(i).WorshipShortName.FirstOrDefault(language)?.Text;

                if (!string.IsNullOrEmpty(s))
                {
                    result = (!string.IsNullOrEmpty(result)) ? $"{result}, {s}" : s;
                }
            }

            return result;
        }
    }
}
