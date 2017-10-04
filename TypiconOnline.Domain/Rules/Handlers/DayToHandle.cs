using System;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Factories;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Используется при обработке правил. Содержит DayService и его объектную реализацию DayContainer
    /// </summary>
    public class DayToHandle
    {
        private DayService _dayService;
        private DayContainer _dayContainer;

        public DayToHandle(DayService dayService)
        {
            DayService = dayService;
        }

        public DayService DayService
        {
            get
            {
                return _dayService;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("DayService");

                value.ThrowExceptionIfInvalid();

                _dayService = value;
                InitializeDayContainer();
            }
        }
        public DayContainer DayContainer
        {
            get
            {
                return _dayContainer;
            }
        }

        private void InitializeDayContainer()
        {
            _dayContainer = new DayContainerFactory(_dayService.DayDefinition).Create();
        }
    }
}