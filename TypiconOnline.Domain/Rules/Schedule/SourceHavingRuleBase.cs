using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public abstract class SourceHavingRuleBase: RuleExecutable, ICalcStructureElement
    {
        private readonly ITypiconSerializer serializer;
        private readonly IWeekDayAppContext weekDayAppContext;

        public SourceHavingRuleBase(string name, ITypiconSerializer serializer, IWeekDayAppContext weekDayAppContext) : base(name)
        {
            this.serializer = serializer ?? throw new ArgumentNullException("serializer in SourceHavingRuleBase");
            this.weekDayAppContext = weekDayAppContext ?? throw new ArgumentNullException("weekDayAppContext in SourceHavingRuleBase");
        }

        /// <summary>
        /// Источник книги, откуда брать текст
        /// </summary>
        public YmnosSource Source { get; set; }
        

        protected DayContainer GetDayContainer(RuleHandlerSettings settings)
        {
            //разбираемся с source
            DayStructureBase dayContainer = null;
            switch (Source)
            {
                case YmnosSource.Item1:
                    dayContainer = (settings.DayWorships.Count > 0) ? settings.DayWorships[0] : null;
                    break;
                case YmnosSource.Item2:
                    dayContainer = (settings.DayWorships.Count > 1) ? settings.DayWorships[1] : null;
                    break;
                case YmnosSource.Item3:
                    dayContainer = (settings.DayWorships.Count > 2) ? settings.DayWorships[2] : null;
                    break;
                case YmnosSource.Oktoikh:
                    dayContainer = settings.OktoikhDay;
                    break;
                case YmnosSource.WeekDay:
                    {
                        var response = weekDayAppContext.Get(new GetWeekDayRequest() { DayOfWeek = settings.Date.DayOfWeek });
                        if (response.Exception == null)
                        {
                            dayContainer = response.WeekDayApp;
                        }
                    }
                    break;
            }

            //if (dayContainer == null)
            //{
            //    throw new KeyNotFoundException("YmnosStructureRule source not found: " + Source.ToString());
            //}

            return dayContainer?.GetElement(serializer);
        }

        public abstract DayElementBase Calculate(RuleHandlerSettings settings);
    }
}
