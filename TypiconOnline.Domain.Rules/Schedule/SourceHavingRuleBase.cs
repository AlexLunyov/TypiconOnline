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
                case YmnosSource.Menology1:
                    dayContainer = (settings.Menologies.Count > 0) ? settings.Menologies[0] : null;
                    break;
                case YmnosSource.Menology2:
                    dayContainer = (settings.Menologies.Count > 1) ? settings.Menologies[1] : null;
                    break;
                case YmnosSource.Menology3:
                    dayContainer = (settings.Menologies.Count > 2) ? settings.Menologies[2] : null;
                    break;
                case YmnosSource.Triodion1:
                    dayContainer = (settings.Triodions.Count > 0) ? settings.Triodions[0] : null;
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
