using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Implementations
{
    public class ScheduleDayNameComposer : IScheduleDayNameComposer
    {
        IOktoikhContext oktoikhContext;

        public ScheduleDayNameComposer(IOktoikhContext oktoikhContext)
        {
            this.oktoikhContext = oktoikhContext ?? throw new ArgumentNullException("IOktoikhContext");
        }

        public ItemTextUnit Compose(RuleHandlerSettings settings, DateTime date)
        {
            //находим самое последнее правило - добавление
            while (settings.Addition != null)
            {
                settings = settings.Addition;
            }

            var result = new ItemTextUnit() { Language = settings.Language.Name };

            if (settings.DayWorships == null || settings.DayWorships.Count == 0)
            {
                return new ItemTextUnit();
            }

            string resultString = "";

            DayWorship seniorService = settings.DayWorships[0];

            //собираем все имена текстов, кроме главного
            if (settings.DayWorships.Count > 1)
            {
                for (int i = 1; i < settings.DayWorships.Count; i++)
                {
                    resultString += settings.DayWorships[i].WorshipName.FirstOrDefault(settings.Language.Name).Text + " ";
                }
            }

            //а теперь разбираемся с главным

            string s = seniorService.WorshipName.FirstOrDefault(settings.Language.Name).Text;

            if (date.DayOfWeek != DayOfWeek.Sunday
                || (date.DayOfWeek == DayOfWeek.Sunday
                    && (seniorService.UseFullName || seniorService.WorshipShortName.IsEmpty)))
            {
                resultString = $"{s} {resultString}";
            }

            int priority = (settings.TypiconRule is Sign sign) ? sign.Priority : settings.TypiconRule.Template.Priority;

            if (/*(settings.Rule is MenologyRule)
                && */(date.DayOfWeek == DayOfWeek.Sunday)
                && (priority > 1))
            {
                //Если Триоди нет и воскресенье, находим название Недели из Октоиха
                //и добавляем название Недели в начало Name

                //Если имеется короткое название, то добавляем только его

                resultString = oktoikhContext.GetSundayName(date, settings.Language.Name,
                    GetShortName(settings.DayWorships, settings.Language.Name)).Text + " " + resultString;

                //жестко задаем воскресный день
                //handlerRequest.Rule = inputRequest.TypiconEntity.Settings.TemplateSunday;
            }

            result.Text = resultString;

            return result;
        }

        public ItemTextUnit GetWeekName(DateTime date, string language) => oktoikhContext.GetWeekName(date, language, false);

        private string GetShortName(List<DayWorship> dayServices, string language)
        {
            string result = "";

            for (int i = 0; i < dayServices.Count; i++)
            {
                string s = dayServices[i].WorshipShortName.FirstOrDefault(language)?.Text;

                if (!string.IsNullOrEmpty(s))
                {
                    result = (!string.IsNullOrEmpty(result)) ? result + ", " + s : s;
                }
            }

            return result;
        }
    }
}
