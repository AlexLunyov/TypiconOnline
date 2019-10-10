using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Interfaces;
using System.Linq;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public class ScheduleHandler : RuleHandlerBase
    {
        private readonly List<OutputWorshipModel> collection = new List<OutputWorshipModel>();
        private readonly ScheduleResults scheduleResults = new ScheduleResults();

        public ScheduleHandler()//(RuleHandlerSettings request) : base(request)
        {
            AuthorizedTypes = new List<Type>()
            {
                typeof(WorshipRule),
                typeof(Notice)
            };

        }

        public override RuleHandlerSettings Settings
        {
            get
            {
                return base.Settings;
            }

            set
            {
                base.Settings = value;

                ClearResult();
            }
        }

        /// <summary>
        /// Укзаание коллекции, куда добавлять элементы последовательностей
        /// </summary>
        public WorshipMode ActualWorshipMode { get; set; }

        public override void ClearResult()
        {
            scheduleResults.Clear();
        }

        public override bool Execute(ICustomInterpreted element)
        {
            if (element is WorshipRule w)
            {
                //задаем актуальную коллекцию
                ActualWorshipMode = w.Mode;
                ActualWorshipCollection.Add(new OutputWorshipModel(w));

                //TODO: удалить
                collection.Add(new OutputWorshipModel(w));

                return true;
            }

            return false;
        }

        public ScheduleResults GetResults() => scheduleResults;
        public ICollection<OutputWorshipModel> ActualWorshipCollection
        {
            get
            {
                ICollection<OutputWorshipModel> col = null;

                switch (ActualWorshipMode)
                {
                    case WorshipMode.DayBefore:
                        col = scheduleResults.DayBefore;
                        break;
                    case WorshipMode.ThisDay:
                        col = scheduleResults.ThisDay;
                        break;
                    case WorshipMode.NextDayFirstWorship:
                        col = scheduleResults.NextDayFirstWorship;
                        break;
                }
                return col;
            }
            
        }

        public OutputSectionModelCollection ActualWorshipChildElements
        {
            get
            {
                var found = ActualWorshipCollection.LastOrDefault();
                
                //если коллекция пуста, добавляем фейковую службу
                if (found == null)
                {
                    found = new OutputWorshipModel();
                    ActualWorshipCollection.Add(found);
                }
                return found.ChildElements;
            }
        }
    }

    public enum WorshipMode
    {
        DayBefore = 0,
        ThisDay = 1,
        NextDayFirstWorship = 2
    }
}
