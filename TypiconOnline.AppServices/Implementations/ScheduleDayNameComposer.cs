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

        public ItemText Compose(DateTime date, int seniorRulePriority, IReadOnlyList<DayWorship> dayWorships)
        {
            var result = new ItemText();

            if (dayWorships == null || dayWorships.Count == 0)
            {
                return result;
            }

            //собираем все имена текстов, кроме главного
            if (dayWorships.Count > 1)
            {
                for (int i = 1; i < dayWorships.Count; i++)
                {
                    result.Merge(dayWorships[i].WorshipName);
                }
            }

            //а теперь разбираемся с главным
            DayWorship seniorService = dayWorships.First();
            var s = seniorService.WorshipName;

            if (date.DayOfWeek != DayOfWeek.Sunday
                || (date.DayOfWeek == DayOfWeek.Sunday
                    && (seniorService.UseFullName || seniorService.WorshipShortName.IsEmpty)))
            {
                result = new ItemText(seniorService.WorshipName).Merge(result);
            }

            //Воскресный день

            if ((date.DayOfWeek == DayOfWeek.Sunday) && (seniorRulePriority > 1))
            {
                //Если Триоди нет и воскресенье, находим название Недели из Октоиха
                //и добавляем название Недели в начало Name

                //Если имеется короткое название, то добавляем только его
                var shortName = GetShortName(dayWorships);

                var sundayName = queryProcessor.Process(new SundayNameQuery(date, shortName));

                result = sundayName.Merge(result);
            }

            return result;
        }

        public ItemTextUnit GetLocalizedWeekName(DateTime date, string language) => GetWeekName(date)?.FirstOrDefault(language);

        public ItemText GetWeekName(DateTime date) => queryProcessor.Process(new WeekNameQuery(date, false));

        private ItemText GetShortName(IReadOnlyList<DayWorship> dayWorships)
        {
            var result = new ItemText();

            foreach (var worship in dayWorships)
            {
                result.Merge(", ", worship.WorshipShortName);
            }

            return result;
        }
    }
}
