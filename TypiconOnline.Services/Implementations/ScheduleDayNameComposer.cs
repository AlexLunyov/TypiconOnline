using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
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

        public string Compose(RuleHandlerSettings settings, DateTime date)
        {
            //находим самое последнее правило - добавление
            while (settings.Addition != null)
            {
                settings = settings.Addition;
            }

            if (settings.DayWorships == null || settings.DayWorships.Count() == 0)
            {
                return string.Empty;
            }

            string result = "";

            DayWorship seniorService = settings.DayWorships[0];

            //собираем все имена текстов, кроме главного
            if (settings.DayWorships.Count > 1)
            {
                for (int i = 1; i < settings.DayWorships.Count; i++)
                {
                    result += settings.DayWorships[i].WorshipName[settings.Language] + " ";
                }
            }

            //а теперь разбираемся с главным

            string s = seniorService.WorshipName[settings.Language];

            if (date.DayOfWeek != DayOfWeek.Sunday
                || (date.DayOfWeek == DayOfWeek.Sunday
                    && (seniorService.UseFullName || seniorService.WorshipShortName.IsEmpty)))
            {
                result = (settings.PutSeniorRuleNameToEnd) ?
                        result + s :
                        s + " " + result;
            }

            int priority = (settings.Rule is Sign sign) ? sign.Priority : settings.Rule.Template.Priority;

            if (/*(settings.Rule is MenologyRule)
                && */(date.DayOfWeek == DayOfWeek.Sunday)
                && (priority > 1))
            {
                //Если Триоди нет и воскресенье, находим название Недели из Октоиха
                //и добавляем название Недели в начало Name

                //Если имеется короткое название, то добавляем только его

                result = oktoikhContext.GetSundayName(date, settings.Language,
                    GetShortName(settings.DayWorships, settings.Language)) + " " + result;

                //жестко задаем воскресный день
                //handlerRequest.Rule = inputRequest.TypiconEntity.Settings.TemplateSunday;
            }

            return result;
        }

        private string GetShortName(List<DayWorship> dayServices, string language)
        {
            string result = "";

            for (int i = 0; i < dayServices.Count; i++)
            {
                string s = dayServices[i].WorshipShortName[language];

                if (!string.IsNullOrEmpty(s))
                {
                    result = (!string.IsNullOrEmpty(result)) ? result + ", " + s : s;
                }
            }

            return result;
        }
    }
}
