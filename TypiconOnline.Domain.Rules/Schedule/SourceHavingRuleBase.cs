using JetBrains.Annotations;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public abstract class SourceHavingRuleBase: RuleExecutable, ICalcStructureElement
    {
        private readonly ITypiconSerializer serializer;
        private readonly IDataQueryProcessor queryProcessor;

        public SourceHavingRuleBase(string name, [NotNull] ITypiconSerializer serializer, [NotNull] IDataQueryProcessor queryProcessor) : base(name)
        {
            this.serializer = serializer;
            this.queryProcessor = queryProcessor;
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
                        dayContainer = queryProcessor.Process(new WeekDayAppQuery(settings.Date.DayOfWeek));
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
